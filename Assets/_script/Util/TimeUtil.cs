using UnityEngine;
using System.Collections;

public class TimeUtil {


	void TimeUti(){


	}

	public static  string getCurrentDay(){

		string curDay = "";

		System.DateTime now = System.DateTime.Now;
		curDay = curDay+now.Date.Year+":"+now.Date.Month+":"+now.Date.Day;


		return curDay;
	}


}
