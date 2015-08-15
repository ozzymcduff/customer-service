require 'albacore'
require 'nuget_helper'

$dir = File.dirname(__FILE__)

desc "Install missing NuGet packages."
task :restore do
  NugetHelper.run_tool ".paket/paket.exe install" 
end

desc "build"
build :build do |msb|
  msb.prop :configuration, :Debug
  msb.prop :platform, "Any CPU"
  msb.target = :Rebuild
  msb.be_quiet
  msb.nologo
  msb.sln =File.join($dir, "CustomerService.sln")
end

task :paket_bootstrap do
  cd ".paket" do
    NugetHelper.run_tool "paket.bootstrapper.exe" 
  end
end

task :paket_restore do
  
end

task :default => ['build']

desc "test using nunit console"
test_runner :test do |nunit|
  nunit.exe = NugetHelper.nunit_path
  files = Dir.glob(File.join($dir,"*Tests","bin","**","*Tests.dll")) 
  nunit.files = files 
end
