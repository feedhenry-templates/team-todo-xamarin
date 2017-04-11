# Xamarin Team To Do Apps

[![AppVeyor](https://img.shields.io/appveyor/ci/feedhenry-templates/team-todo-xamarin/master.svg)](https://ci.appveyor.com/project/feedhenry/team-todo-xamarin/) 

## Overview

Example project to demostrate how to develop cloud-connected native cross-platform apps using [Xamarin](http://xamarin.com/) and [FeedHenry .NET SDK](https://github.com/feedhenry/fh-dotnet-sdk). 

NOTE: This project only contains the client apps code, and it is using the [FeedHenry TeamToDo Cloud App](https://github.com/feedhenry-templates/team-todo-cloud) as the cloud back end. If you are interested in the cloud code, please check the cloud app's repo instead.

## App Functions

The basic function implemented for the apps:

* User login
* List all the tasks that are assigned to the current logged in user
* Allow user to update task details
* Allow user to create new tasks and assign to other users

Based on the requirements of the app, we divide the apps into following layers:

* Service Access Layer - e.g. user login, task listing & CRUD operations using RSETFul endpoints
* Business Layer - e.g. Define User & Task Models, login to manage users and tasks
* Application Layer - e.g. ios/android specific application logic
* UI Layer - e.g. UI for ios and android

and here is the architecture diagram:

## Architecture

![](https://raw.githubusercontent.com/feedhenry-templates/team-todo-xamarin/master/images/architecture.png)


## ToDos

* Add WP Sample
* Add Map View to allow choose task location



