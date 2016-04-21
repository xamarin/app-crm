require "benchmark"
require "date"
require 'net/http'

### PROPERTIES TO SET
APP_NAME = "Xamarin CRM"

ANDROID_DIR = "../../src/MobileApp/XamarinCRM.Android"
IOS_DIR = "../../src/MobileApp/XamarinCRM.iOS"
TEST_DIR = "../../src/MobileApp/XamarinCRM.UITest"
PACKAGE_DIR = "../../src/MobileApp/packages/Xamarin.UITest.1.2.0"

SLN_FILE = "../../src/MobileApp/XamarinCRM.sln"
APK_FILE = "../../src/MobileApp/XamarinCRM.Android/bin/Release/com.xamarin.xamarincrm-Signed.apk"
IPA_FILE = "../../src/MobileApp/XamarinCRM.iOS/bin/iPhone/Debug/XamarinCRM_Debug.ipa"
DSYM_FILE = "../../src/MobileApp/XamarinCRM.iOS/bin/iPhone/Debug/XamarinCRMiOS.app.dSYM"

# ANDROID_KEYSTORE = "debug.keystore"

DEFAULT_SERIES = "master"
DEFAULT_IOS_DEVICE_SET = "2f802e3f" # small set
DEFAULT_ANDROID_DEVICE_SET = "fe5e138d" # small set
### END

task :default => ['build:android', 'build:ios', 'build:tests']

desc "Removes bin and obj directories for Android, iOS, and test projects."
task :clean do
  [ANDROID_DIR, IOS_DIR, TEST_DIR].each do |dir|
    rm_rf "#{dir}/bin"
    rm_rf "#{dir}/obj"
  end
end

namespace :build do
  desc "Builds the Android project"
  task :android => [:restore_packages] do
    # puts "Adding maps to manifest"
		# addMaptoManifest("../../src/MobileApp/XamarinCRM.Android/Properties/AndroidManifest.xml")
    puts "building Android project with:"
    time = time_cmd "xbuild #{ANDROID_DIR}/*.csproj /p:Configuration=Release /t:SignAndroidPackage /verbosity:quiet /flp:LogFile=build_android.log" # /verbosity:quiet
    size = (File.size(APK_FILE)/1000000.0).round(1)
    log_data "Android", time, size, "build_android.log"
  end

  desc "Builds the iOS project"
  task :ios => [:restore_packages] do
    puts "building iOS project with:"
    time = time_cmd "xbuild #{IOS_DIR}/*.csproj /p:Configuration=Debug /p:Platform=iPhone /p:OutputPath='bin/iPhone/Debug/' /verbosity:quiet /flp:LogFile=build_ios.log" # /verbosity:quiet
    size = (File.size(IPA_FILE) / 1000000.0).round(1)
    log_data "iOS", time, size, "build_ios.log"
  end

  desc "Builds the test project"
  task :tests => [:restore_packages] do
    puts "building UITest project with:"
  	sh "xbuild #{TEST_DIR}/*.csproj /p:Configuration=Debug /verbosity:quiet"
  end

  desc "Restores packages for all projects"
  task :restore_packages do
    puts "restoring packages with:"
    sh "nuget restore #{SLN_FILE}"
  end

  def time_cmd(cmd)
    time = Benchmark.realtime do
      sh cmd
    end
    min = (time / 60).to_i.to_s
    sec = (time % 60).to_i.to_s
    sec = sec.length < 2 ? "0" + sec : sec
    return "#{min}:#{sec}"
  end

  def log_data(platform, time, size, log_file)
    date = DateTime.now.strftime("%m/%d/%Y %I:%M%p")
    version = /\d+\.\d+\.\d+\.\d+/.match(`mdls -name kMDItemVersion /Applications/Xamarin\\ Studio.app`)
    user = /\w+$/.match(ENV['HOME'])[0].capitalize

    tail = `tail -n 6 #{log_file}`
    warnings = /(\d+) Warning\(s\)/.match(tail).captures[0]
    errors = /(\d+) Error\(s\)/.match(tail).captures[0]

    puts "*** origin: #{user}"
    puts "*** xamarin version: #{version}"
    puts "*** platform: #{platform}"
    puts "*** date time: #{date}"
    puts
    puts "*** build time: #{time}"
    puts "*** app size (MB): #{size}"
    puts "*** warnings: #{warnings}"
    puts "*** errors: #{errors}"

    post_to_sheet(date, platform, user, version, size, time, warnings, errors)
  end

  def post_to_sheet(date, platform, channel, version, size, time, warnings, errors)
		return unless ENV['POST_RESULTS'] == "true"

    uri = URI("https://script.google.com/macros/s/AKfycbzlrwTXjCjOHY64uOIF3C1yg1GYDpvK8XXcDBfX68c6YhkL21M/exec")
    params = {
      method: 'writeLine',
      destSheet: 'Log',
      date: date,
      platform: platform,
      channel: channel,
      version: version,
      app: APP_NAME,
      size:size,
      time: time,
      warnings: warnings,
      errors: errors
    }

    res = Net::HTTP.post_form(uri, params)
    if res.code == "302"
      res = Net::HTTP.get_response(URI(res.header['location']))
    end
    raise res.body unless res.is_a?(Net::HTTPSuccess)

    puts res.body
  end

  # Only CRM
	# def addMaptoManifest(xml_file)
	#   xml_text = File.read(xml_file)
  #
	#   newContent = xml_text.gsub("\t\t<meta-data android:name=\"com.google.android.maps.v2.API_KEY\" android:value=\"@string/GoogleMapsKey\" />\n",
	#       "\t\t<meta-data android:name=\"com.google.android.maps.v2.API_KEY\" android:value=\"AIzaSyBmRuR-M2PV8bF_ljjAQBNzkzSDpmkStfI\" />\n")
  #
	#   File.open(xml_file, "w"){|newFile| newFile.puts newContent}
	# end
end

namespace :submit do
  desc "Submits Android app to Test Cloud"
  task :android => ['build:tests'] do
    args = get_submit_args "android"

    puts "uploading Android tests to Test Cloud with:"
    submit_file_with_extra_params APK_FILE, args
  end

  desc "Submits iOS app to Test Cloud"
  task :ios => ['build:tests'] do
    args = get_submit_args "ios"

    extras = "--dsym #{DSYM_FILE}"

    puts "uploading iOS tests to Test Cloud with:"
    submit_file_with_extra_params IPA_FILE, args, extras
  end

  def submit_file_with_extra_params(file, args, extras="")
    cmd = "mono #{PACKAGE_DIR}/tools/test-cloud.exe submit #{file} #{args[:api_key]} --devices #{args[:device_set]} --series '#{args[:series]}' --locale en_US --app-name '#{APP_NAME}' --user #{args[:user_account]} --assembly-dir #{TEST_DIR}/bin/Debug"

    cmd += " --category #{ENV['CATEGORY']}" unless ENV['CATEGORY'].nil?
    cmd += " --fixture #{ENV['FIXTURE']}" unless ENV['FIXTURE'].nil?
    cmd += " --async" if ENV['ASYNC'] == "true"

    cmd += " #{extras}"

    sh cmd
  end

  def get_submit_args(platform)
    args = {}

    args[:user_account] =  ENV['USER_ACCOUNT']
    args[:api_key] = ENV['API_KEY']

    args.each do |arg, val|
      raise "ERROR: You must specify the '#{arg}' environment variable" if val.nil?
    end

    args[:series] = ENV['SERIES'] || DEFAULT_SERIES

    case platform
    when "android"
      args[:device_set] = ENV['ANDROID_DEVICE_SET'] || DEFAULT_ANDROID_DEVICE_SET
    when "ios"
      args[:device_set] = ENV['IOS_DEVICE_SET'] || DEFAULT_IOS_DEVICE_SET
    end

    return args
  end
end
