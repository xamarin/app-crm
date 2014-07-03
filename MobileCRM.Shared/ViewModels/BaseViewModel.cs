using MobileCRM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MobileCRM.Shared.ViewModels
{
  public abstract class BaseViewModel : INotifyPropertyChanged
  {
    private IEnumerable<IUser> users = new IUser[0];
    private string title = string.Empty;

    /// <summary>
    /// Gets or sets the "Users" property.
    /// </summary>
    /// <value>The users.</value>
    public const string UsersPropertyName = "Users";
    public IEnumerable<IUser> Users
    {
      get { return users; }
      set { SetProperty(ref users, value, UsersPropertyName); }
    }

    /// <summary>
    /// Gets or sets the "Title" property
    /// </summary>
    /// <value>The title.</value>
    public const string TitlePropertyName = "Title";
    public string Title
    {
      get { return title; }
      set { SetProperty(ref title, value, TitlePropertyName); }
    }

    private string subTitle = string.Empty;
    /// <summary>
    /// Gets or sets the "Subtitle" property
    /// </summary>
    public const string SubtitlePropertyName = "Subtitle";
    public string Subtitle
    {
      get { return subTitle; }
      set { SetProperty(ref subTitle, value, SubtitlePropertyName); }
    }

    private string icon = null;
    /// <summary>
    /// Gets or sets the "Icon" of the viewmodel
    /// </summary>
    public const string IconPropertyName = "Icon";
    public string Icon
    {
      get { return icon; }
      set { SetProperty(ref icon, value, IconPropertyName); }
    }

    protected void SetProperty<U>(
        ref U backingStore, U value,
        string propertyName,
        Action onChanged = null,
        Action<U> onChanging = null)
    {
      if (EqualityComparer<U>.Default.Equals(backingStore, value))
        return;

      if (onChanging != null)
        onChanging(value);

      OnPropertyChanging(propertyName);

      backingStore = value;

      if (onChanged != null)
        onChanged();

      OnPropertyChanged(propertyName);
    }

    #region INotifyPropertyChanging implementation
    public event Xamarin.Forms.PropertyChangingEventHandler PropertyChanging;
    #endregion

    public void OnPropertyChanging(string propertyName)
    {
      if (PropertyChanging == null)
        return;

      PropertyChanging(this, new Xamarin.Forms.PropertyChangingEventArgs(propertyName));
    }


    #region INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    public void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged == null)
        return;

      PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
