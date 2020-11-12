namespace Atlas
{
    /// <summary>
    /// 	A function that is a full (no parameters dropped) implementation of <see cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>.
    /// </summary>
    /// <param name="services">The services available in this service request.</param>
    /// <param name="context">Information about the service request.</param>
    public delegate TService WholeBindingImpl<out TService, in TContext>(IServiceResolver services, TContext context);

    /// <summary>
    /// 	A function that is a recursive (resolver only) implementation of <see cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>.
    /// </summary>
    /// <param name="services">The services available in this service request.</param>
    public delegate TService RecursiveBindingImpl<out TService>(IServiceResolver services);

    /// <summary>
    /// 	A function that is a contextual (context only) implementation of <see cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>.
    /// </summary>
    /// <param name="context">Information about the service request.</param>
    public delegate TService ContextualBindingImpl<out TService, in TContext>(TContext context);

    /// <summary>
    /// 	A function that is a pure (no parameters taken) implementation of <see cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>.
    /// </summary>
    public delegate TService PureBindingImpl<out TService>();
}