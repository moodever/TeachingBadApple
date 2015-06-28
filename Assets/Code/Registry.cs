using System;
using System.Collections;
using UnityEngine;

public class Registry {

	private Hashtable table;

	public static Registry INSTANCE = new Registry();

	private Registry() {
		table = new Hashtable ();
	}

	public static object get(string key) {
		lock (INSTANCE) {
			return INSTANCE.table [key];
		}
	}

	public static void put(string key, object value) {
		lock (INSTANCE) {
			INSTANCE.table[key] = value;
		}
	}

	public static int getInt(string key) {
		return int.Parse(get (key).ToString());
	}
}
