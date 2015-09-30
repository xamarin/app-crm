task :default => ['build:ios', 'build:android']

namespace :build do
	desc "Build iOS App"
	task :ios do
		sh "pwd"
		sh "nuget restore src/MobileApp/XamarinCRM.iOS/packages.config -PackagesDirectory src/MobileApp/packages/"
		sh "nuget restore src/MobileApp/XamarinCRM.UITest/packages.config -PackagesDirectory src/MobileApp/packages/"
		puts "iOS nugets restored"
		puts
		puts "Building XamarinCRM.iOS"
		sh "xbuild src/MobileApp/XamarinCRM.sln /p:Configuration=Debug /p:Platform=iPhone /p:BuildIpa=true"
		puts
		puts "Checking for Insights on iOS"
		sh "cat src/MobileApp/XamarinCRM.iOS/Main.cs | grep Insights | grep 2b82ddc37582e6c1bece7e5901e8bae3bf7bfb26"
		puts
		puts "iOS BUILD SUCCESSFUL"
	end

	desc "Build Android App"
	task :android do
		sh "nuget restore src/MobileApp/XamarinCRM.Android/packages.config -PackagesDirectory src/MobileApp/packages"
		puts "Android nugets restored"
		puts
		addMaptoManifest("src/MobileApp/XamarinCRM.Android/Properties/AndroidManifest.xml")
		puts "Maps added to manifest"
		puts "Building XamarinCRM.Android"
		sh "xbuild /t:SignAndroidPackage /p:Configuration=Release src/MobileApp/XamarinCRM.Android/XamarinCRM.Android.csproj"
		puts
		puts "Checking for Insights on Android"
		sh "cat src/MobileApp/XamarinCRM.Android/MainActivity.cs | grep Insights | grep 2b82ddc37582e6c1bece7e5901e8bae3bf7bfb26"
		puts
		puts "Android BUILD SUCCESSFUL"
	end
end

namespace :testcloud do
	desc "Build Test Solution"
	task :build_test_solution do
		puts "Restoring Test Nuget Packages"
		sh "nuget restore src/MobileApp/XamarinCRM.UITest/packages.config -PackagesDirectory src/MobileApp/packages/"
		puts "Building Test Solution"
		sh "xbuild src/MobileApp/XamarinCRM.UITest/XamarinCRM.UITest.csproj"

		API_KEY = ENV['API_KEY']
		USER_ACCOUNT = ENV['USER_ACCOUNT']
		DEVICE_SET = ENV['DEVICE_SET']

		arguments = ["API_KEY", "USER_ACCOUNT", "DEVICE_SET"]

		[API_KEY, USER_ACCOUNT, DEVICE_SET].each_with_index do |val, index|
			if val.nil?
				raise "#{arguments[index]} cannot be nil, please input a value."
			end
		end

		LOCALE = "en_US"
	end

	desc "Push iOS tests to XTC"
	task :ios => [:build_test_solution] do
		devices = get_device_code(DEVICE_SET, "ios")
		sh "mono src/MobileApp/packages/Xamarin.UITest.1.1.1/tools/test-cloud.exe submit src/MobileApp/XamarinCRM.iOS/bin/iPhone/Debug/*.ipa #{API_KEY} --devices #{devices} --series \"#{DEVICE_SET}\" --locale #{LOCALE} --app-name \"Xamarin CRM\" --user #{USER_ACCOUNT} --assembly-dir src/MobileApp/XamarinCRM.UITest/bin/Debug --dsym src/MobileApp/XamarinCRM.iOS/bin/iPhone/Debug/*.app.dSYM"
	end

	desc "Push Android tests to XTC"
	task :android => [:build_test_solution] do
		devices = get_device_code(DEVICE_SET, "android")
		sh "mono src/MobileApp/packages/Xamarin.UITest.1.1.1/tools/test-cloud.exe submit src/MobileApp/XamarinCRM.Android/bin/Release/*Signed.apk #{API_KEY} keystore ~/.android/debug.keystore android androiddebugkey android --devices #{devices} --series \"#{DEVICE_SET}\" --locale #{LOCALE} --app-name \"Xamarin CRM\" --user #{USER_ACCOUNT} --assembly-dir src/MobileApp/XamarinCRM.UITest/bin/Debug"
	end
end

def get_device_code device_set, platform
	case platform
	when "android"
		case device_set
		when "Small"
			return 'fe5e138d'
		when "Medium"
			return 'f3836139'
		when "Large"
			return "ac078209"
		end
	when "ios"
		case device_set
		when "Small"
			return '2f802e3f'
		when "Medium"
			return '588fda26'
		when "Large"
			return 'e1df384a'
		end
	end
end

def addMaptoManifest(xml_file)
    xml_text = File.read(xml_file)

    newContent = xml_text.gsub("\t\t<meta-data android:name=\"com.google.android.maps.v2.API_KEY\" android:value=\"@string/GoogleMapsKey\" />\n",
        "\t\t<meta-data android:name=\"com.google.android.maps.v2.API_KEY\" android:value=\"AIzaSyBmRuR-M2PV8bF_ljjAQBNzkzSDpmkStfI\" />\n")

    File.open(xml_file, "w"){|newFile| newFile.puts newContent}
end