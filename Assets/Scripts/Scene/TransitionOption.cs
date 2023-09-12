namespace Scene
{
  public struct TransitionOption
  {
    public string type;

    public float speed;

    public float delay;

    public TransitionOption(string type, float speed = 1f, float delay = 0f)
    {
      this.type = type;
      this.speed = speed;
      this.delay = delay;
    }

    public static implicit operator TransitionOption(string str) => new(str);
  }
}