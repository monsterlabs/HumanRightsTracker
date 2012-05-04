# Human Rights Tracker 1.x

##. License

Licensed under GPL 2.1. See LICENSE.txt for detail

## Basic requirements to build the application

* MonoDevelop 2.8.6.5 or Higher
* Mono MDK installer (Mono 2.10.9)
* SQLite3
* Ruby and gems

## Requeriments to build the database and the application for Mac

* RVM (Ruby Version Manager)
* ruby 1.9.3-p125
* Bundler 1.0.14 or Higher 
* Gems listed in each Gemfile file

## How to build the database

Before to compile the application you must build the database executing the following commands:

	$ cd tools/database
	$ rake drop_db
	$ rake migrate
	$ rake seeds  

If you want to populate your database for demo, you can run this rake task:

	$ rake seeds_demo

Note: You can comment some lines at the end of the Rakefile file to generate an empty database.

## How to build the bundle for mac 

After you build your application in Monodevelop, you can build your app bundle with the following commands.

	$ cd build/macos
	$ ./appifier appify

## How to build the ubuntu package

After you build your application in Monodevelop, you must generate the package from the menu options:
Project -> Create Package, after that you will need to execute the
following instructions:

	$ cd build/linux
	$ ./appifier clean
	$ ./appifier tarball
	$ ./appifier build_pkg

## How to build the msi package for windows

Basic requeriments are: Windows7, .Net 3.5, MonoDevelop, CYGWIN and Git.

  * Build your project in mac or linux to generate the DLL files and compiled po (.mo) files.

  * Clone the project from this repository into the windows machine

  	$ git clone  git://github.com/monsterlabs/HumanRightsTracker.git
  
  * Clone the set of libraries from the windows-binaries repository in to the bin directory and name this as bin

  	$ cd bin
	$ git clone git://github.com/monsterlabs/windows-binaries.git bin
	
  * Copy the compiled po (.mo) file in to the directory bin/bin/locale/po/LC_MESSAGES  
  * Compile the project in MonoDevelop (Change the size of LoginWindows.cs view to make it and save the file)
  * Copy the compiled DLL and po files to bin/bin directory
  * Run the application in MonoDevelop and verify that its working well
  * Edit the files  Installer.wixproj and InstallerDefinition.wxs to set the packager version.
  * Execute the build-installer.js script (Doing double click under this file).

## Authors

  * [Alejandro Juárez Robles] (https://github.com/juarlex)
  * [Juan G. Castañeda Echevarria] (https://github.com/juanger)
  * [Hector E. Gómez Morales] (https://github.com/hectoregm)

## Sponsored By
  * [Idheas, Litigio Estratégico en Derechos Humanos, A.C.] (http://www.idheas.org/)
