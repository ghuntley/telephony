![](http://www.netanimations.net/yeild-to-animated-construction-guy.gif)
![](http://i.imgur.com/xsRdz22.png)
![](http://www.netanimations.net/yeild-to-animated-construction-guy.gif)
# Telephony 

Telephony will be a xplat library that provides email, sms, voice and video call functionality. 

# Supported Platforms

* Xamarin.iOS
* Xamarin.Android
* Windows Phone 8/8.1 (Silverlight)
* Windows Phone 8.1 (WPA)

# Installation
Installation is done via NuGet:

    Install-Package Telephony
        	
# Usage

Within your core library:

    using Telephony;
    using ReactiveUI;
    using Conditions;
   	
    public ReactiveCommand<string> DialPhoneNumber { get; private set; }
    public ReactiveCommand<string> ComposeMessage { get; private set; }
    private readonly _telephony;

    ExampleViewModel(ITelephony telephony)
    {
    	// Cool stuff: Conditions is cross platform portable class library helps
    	// developers to write pre- and postcondition validations in a fluent
        // manner. Usage is completely optional and this is here to act as a
        // safety step/tear down the house if you forget to register an
        // implementation of the TelephoneyService<T> 
        Condition.Requires(telephone).IsNotNull();
        
        _telephony = telephony;
        
        // ReactiveUI is completely optional but you totally should be using it
        // as once you experience building a mobile application declaratively
        // using the Reactive Extensions theres no going back. 
        DialPhoneNumber = ReactiveCommand.Create();
       	
       	DialPhoneNumber.Subscribe(phoneNumber => 
       	{
       		// This is telephony, choose between either video or voice.
       		_telephony.MakePhoneCall(phoneNumber);
       		_telephony.MakeVideoCall(phoneNumber);
       	});
       	
    	DialPhoneNumber.ThrownExceptions
            .Subscribe(ex => {
                UserError.Throw("Is Airplane mode enabled?", ex);
        });

        // ReactiveUI is completely optional but you totally should be using it
        // as once you experience building a mobile application declaratively
        // using the Reactive Extensions theres no going back. 
        ComposeMessage = ReactiveCommand.Create();
       	
       	ComposeMessage.Subscribe(phoneNumber => 
       	{
       		// This is telephony, choose between either SMS or email.
       		_telephony.ComposeSMS(phoneNumber);
       		_telephony.ComposeEmail(new Email("hello@contoso.com, hello@adventure-works.com"));
       	});
       	
    	ComposeMessage.ThrownExceptions
            .Subscribe(ex => {
                UserError.Throw("Is messaging enabled and are there accounts configured?", ex);
        });
    }

Within the application initialization for each platform:
    	
    container.Register(c => new TelephonyService()).As<ITelephonyService>();

## With thanks to
* The icon "<a href="http://thenounproject.com/term/telephone/77246/" target="_blank">Telephone</a>" designed by <a href="http://thenounproject.com/sven-gabriel" target="_blank">Sven Gabriel</a> from The Noun Project.
