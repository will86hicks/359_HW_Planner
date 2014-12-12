package hwplannerandroid.screens;


public class HWDetailsScreen
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onCreateDialog:(I)Landroid/app/Dialog;:GetOnCreateDialog_IHandler\n" +
			"";
		mono.android.Runtime.register ("HWPlannerAndroid.Screens.HWDetailsScreen, HWAndroid, Version=1.0.5458.42288, Culture=neutral, PublicKeyToken=null", HWDetailsScreen.class, __md_methods);
	}


	public HWDetailsScreen () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HWDetailsScreen.class)
			mono.android.TypeManager.Activate ("HWPlannerAndroid.Screens.HWDetailsScreen, HWAndroid, Version=1.0.5458.42288, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public android.app.Dialog onCreateDialog (int p0)
	{
		return n_onCreateDialog (p0);
	}

	private native android.app.Dialog n_onCreateDialog (int p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
