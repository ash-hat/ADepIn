namespace Atlas
{
	/// <summary>
	/// 	Represents a loadable module which can be reflectively loaded.
	/// </summary>
	public interface IEntryModule<TSelf> : IModule where TSelf : IEntryModule<TSelf>, new()
	{
	}
}
