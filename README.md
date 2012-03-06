# Human Rights Tracker 1.x

##. License

Licensed under GPL 2.1. See LICENSE.txt for detail

## Basic requirements to build the application

* MonoDevelop 2.8.6.5 or Higher
* Mono MDK installer (Mono 2.10.9)
* SQLite3
* Ruby and gems

## Requeriments to build Database and the application for Mac

* RVM (Ruby Version Manager)
* ruby 1.9.2-p290
* Bundler 1.0.14
* Gems listed in each Gemfile file

## How to build the database

Before to compile the application you must build the database executing the following commands.

$ cd tools/database
$ rake migrate
$ rake seeds  

Note: You can comment some lines at the end of the Rakefile file to generate an empty database.

## How to build the bundle for mac 

After you build your application in Monodevelop, you can build your app bundle with the following commands.

$ cd tools/appifier
$ ./appifier appify

## How to build the msi package for windows
