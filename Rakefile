# encoding: utf-8
require 'albacore'
require 'rake'
require 'rubygems'

VERSION = "0.3.0.3"

# http://jasonseifer.com/2010/04/06/rake-tutorial

task :default => [:compile, :package]

Albacore.configure do |config|
	config.log_level = :verbose
	config.msbuild.use :net35
	
	desc "Compiles and builds installer project"
	exec :clean do |cmd|
		cmd.command = "C:/Program Files/Microsoft Visual Studio 9.0/Common7/IDE/devenv.com"
		cmd.parameters = "src/FalconerDevelopment.MantisConnect.sln /Clean"
	end
	
	desc "Compiles installer project"
	exec :compile => [:clean, :assemblyinfo] do |cmd|
		cmd.command = "C:/Program Files/Microsoft Visual Studio 9.0/Common7/IDE/devenv.com"
		cmd.parameters = 'src/FalconerDevelopment.MantisConnect.sln /Build "Release|Any CPU"'
	end
	
	desc "Run a sample assembly info generator"
	assemblyinfo :assemblyinfo do |asm|
	  asm.version = VERSION
	  asm.file_version = VERSION
	  asm.company_name = "Falconer Development LLC"
	  asm.product_name = "FalconerDevelopment Mantis Connect"
	  asm.copyright = "Copyright Falconer Development LLC 2011"
	  asm.output_file = "src/ProductAssemblyInfo.cs"
	end
	
	desc "Package build output"
	zip :package do |zip|
	  zip.directories_to_zip "./build/Release"
	  zip.output_file = "FalconerDevelopment.MantisConnect-#{VERSION}.zip"
	end
	
	desc "Clean any extra build artifacts before building"
	exec :clean_release do |cmd|
		cmd.command = "del"
		cmd.parameters = "/Q /F ./build/Release"
	end
end
