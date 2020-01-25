using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_Register_Player : MonoBehaviour {


	[SerializeField]
	public Text username, password;

	

	public void RegisterPlayerbtn()
	{
		new GameSparks.Api.Requests.RegistrationRequest()
			.SetDisplayName(username.text)
			.SetUserName(username.text)
			.SetPassword(password.text)
			.Send((response) =>
			{
				if (response.HasErrors)
					print("ERROR WITH REGISTERATION \n"+response.Errors.JSON.ToString());
				else
					Debug.Log("REGISTARTION COMPLETE \n" + response.DisplayName);
			});
	}

	public void AuthorizePlayerbtn()
	{
		new GameSparks.Api.Requests.AuthenticationRequest()
			.SetUserName(username.text)
			.SetPassword(password.text)
			.Send((response) => {
			if (!response.HasErrors)
			{
				Debug.Log("Player Authenticated... username : " + response.DisplayName );
			}
			else
			{
				Debug.Log("Error Authenticating Player... \n "+response.Errors.JSON.ToString());
			}
		});
	}
	
}
