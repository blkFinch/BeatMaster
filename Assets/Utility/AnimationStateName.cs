/*
    This class contains the strings for the various animation states.
    This allows ease of use for the IsometricPlayerRenderer
*/
public class AnimationStateName{
    private AnimationStateName(string value){ Value = value; }

    public string Value {get; set;}

    //Idle States
    public static AnimationStateName IdleNE {get { return new AnimationStateName("Idle NE");} }
    public static AnimationStateName IdleNW {get { return new AnimationStateName("Idle NW");} }
    public static AnimationStateName IdleSE {get { return new AnimationStateName("Idle SE");} }
    public static AnimationStateName IdleSW {get { return new AnimationStateName("Idle SW");} }

    //Run states
    public static AnimationStateName RunNE {get { return new AnimationStateName("Run NE");} }
    public static AnimationStateName RunNW {get { return new AnimationStateName("Run NW");} }
    public static AnimationStateName RunSE {get { return new AnimationStateName("Run SE");} }
    public static AnimationStateName RunSW {get { return new AnimationStateName("Run SW");} }
}