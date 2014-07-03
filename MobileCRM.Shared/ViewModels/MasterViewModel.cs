using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;
using MobileCRM.Services;
using MobileCRM.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MobileCRM.Shared.ViewModels
{
    public class MasterViewModel<T> : BaseViewModel
        where T: class, IContact, new()
    {
        // Analysis disable once StaticFieldInGenericType
        static readonly MethodInfo GetDependency;

        static MasterViewModel() 
        {
            // NOTE: Workaround lack of generics support in DependencyService,
            //          i.e. because we can't just do: DependencyService.Get<IRepository<T>>()
            // NOTE: Refactor to a Factory/Builder pattern.
            var repoType = MobileCRMApp.TypeMap[typeof(T)];
            var getMethod = typeof(DependencyService)
                .GetRuntimeMethods()
                .Single((method)=>
                    method.Name.Equals("Get"));
            GetDependency = getMethod.MakeGenericMethod(repoType);
        }

        const string IconFormat = "{0}.png";

        private T selectedModel = null;

        public MasterViewModel()
        {
            Title = typeof(T).Name;
            Icon = string.Format(IconFormat, Title).ToLower() ;
            Models = new ObservableCollection<T>();
        }

        /// <summary>
        /// Gets or sets the "Users" property.
        /// </summary>
        /// <value>The users.</value>
        public const string SelectedModelPropertyName = "SelectedModel";
        public T SelectedModel
        {
            get { return selectedModel; }
            set { SetProperty(ref selectedModel, value, SelectedModelPropertyName); }
        }

        private Command saveSelectedModel;
        public Command SaveSelectedModel
        {
            get
            {
                return saveSelectedModel ?? (saveSelectedModel = new Command(ExecuteSaveSelectedModel));
            }
        }

        protected virtual async void ExecuteSaveSelectedModel()
        {
            using (var service = (IRepository<T>)GetDependency.Invoke(null, new object[] { DependencyFetchTarget.GlobalInstance }))
            {
                var model = await service.Update(SelectedModel);
                SelectedModel = model; // In case any updates propagate from the repository (e.g. updated id, etc.).
            }
        }

        public ObservableCollection<T> Models { get; private set; }

        private Command loadModelsCommand;
        public Command LoadModelsCommand
        {
            get
            {
                return loadModelsCommand ?? (loadModelsCommand = new Command(ExecuteLoadModelsCommand));
            }
        }

        protected virtual async void ExecuteLoadModelsCommand()
        {
            using (var service = (IRepository<T>)GetDependency.Invoke(null, new object[] { DependencyFetchTarget.GlobalInstance }))
            {
                var models = await service.All();

                foreach(var model in models)
                    Models.Add(model);
            }
            OnPropertyChanged("Models");

            using (var service = DependencyService.Get<UserRepository>())
            {
                var users = await service.All();
                Users = users;
            }

        }
    }

   
}
