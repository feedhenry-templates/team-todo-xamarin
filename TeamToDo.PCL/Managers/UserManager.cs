using System;
using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TeamToDo.PCL
{
	/// <summary>
	/// Manage users
	/// </summary>
	public class UserManager
	{
		private User currentUser;
		private static UserManager manager;

		/// <summary>
		/// Accessing the UserService provider
		/// </summary>
		/// <value>The user service provider.</value>
		public UserService  UserServiceProvider {
			set;
			get;
		}

		private UserManager ()
		{
			this.UserServiceProvider = new UserService ();
		}


		/// <summary>
		/// Return the singleton UserManager
		/// </summary>
		/// <returns>The instance.</returns>
		public static UserManager GetInstance()
		{
			if (null == manager) {
				manager = new UserManager ();
			}
			return manager;
		}

		/// <summary>
		/// Return the current logged in user
		/// </summary>
		/// <value>The current logged in.</value>
		public User CurrentUser {
			get {
				return currentUser;
			}
		}

		/// <summary>
		/// Login a user
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <param name="password">Password.</param>
		public async Task Login(string userName, string password)
		{
			Contract.Assert (null == currentUser, "User is already logged in");
			Contract.Assert(null != userName, "Username is null");
			Contract.Assert(null != password, "Password is null");

			User user = new User {
				Username = userName, 
				Password = password
			};

			try 
			{
				await this.UserServiceProvider.Login(user);
				currentUser = user;
			} catch (Exception e)
			{
				currentUser = null;
				throw e;
			}
		}

		/// <summary>
		/// Logout a user
		/// </summary>
		public async Task Logout()
		{
			Contract.Assert (null != currentUser, "User is not logged in");

			try
			{
				await this.UserServiceProvider.Logout(currentUser);
				currentUser = null;
			} catch (Exception e) 
			{
				currentUser = null;
			}
		}

		/// <summary>
		/// Get the current logged in user's session id
		/// </summary>
		/// <returns>The session identifier.</returns>
		public string GetSessionId()
		{
			Contract.Assert (null != currentUser, "User is not logged in");
			return currentUser.Session.SessionId;
		}

		/// <summary>
		/// List all the users
		/// </summary>
		/// <returns>The users.</returns>
		public async Task<List<User>> ListUsers()
		{
			Contract.Assert (null != currentUser, "User is not logged in");
			return await this.UserServiceProvider.ListUsers (currentUser.Session);
		}



	}
}

