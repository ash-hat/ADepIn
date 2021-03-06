namespace ADepIn
{
	/// <summary>
	/// 	Represents the highest level of access to a collection of services.
	/// </summary>
	public interface IServiceKernel : IServiceBinder, IServiceResolver
	{
	}
}
