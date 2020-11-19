namespace ADepIn
{
	/// <summary>
	/// 	Represents a loadable module.
	/// </summary>
	public interface IModule
	{
		/// <summary>
		/// 	Loads this module and associated objects into an <see cref="IServiceKernel" />.
		/// </summary>
		/// <param name="kernel">The kernel to load this module into.</param>
		void Load(IServiceKernel kernel);
	}
}
