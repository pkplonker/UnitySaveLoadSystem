namespace Save
{
    /// <summary>
    /// Interface <c>ISaveLoad</c> for components that require data to be saved
    /// </summary>
    public interface ISaveLoad
    {
        public void LoadState(object data);
        public object SaveState();

       

    }
}
