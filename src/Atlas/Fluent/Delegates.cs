namespace Atlas
{
    /// <summary>
    /// 	A function that is a full (no parameters dropped) implementation of <see cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>.
    /// </summary>
    /// <param name="services">The services available in this service request.</param>
    /// <param name="context">Information about the service request.</param>
    public delegate Option<TService> WholeBindingImpl<TService, in TContext>(IServiceResolver services, TContext context);

    /// <summary>
    /// 	A function that is a recursive (resolver only) implementation of <see cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>.
    /// </summary>
    /// <param name="services">The services available in this service request.</param>
    public delegate Option<TService> RecursiveBindingImpl<TService>(IServiceResolver services);

    /// <summary>
    /// 	A function that is a contextual (context only) implementation of <see cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>.
    /// </summary>
    /// <param name="context">Information about the service request.</param>
    public delegate Option<TService> ContextualBindingImpl<TService, in TContext>(TContext context);

    /// <summary>
    /// 	A function that is a pure (no parameters taken) implementation of <see cref="IServiceBinding{TService, TContext}.Get(IServiceResolver, TContext)"/>.
    /// </summary>
    public delegate Option<TService> PureBindingImpl<TService>();

    public delegate TService WholeNopBindingImpl<out TService, in TContext>(IServiceResolver services, TContext context);

    public delegate TService RecursiveNopBindingImpl<out TService>(IServiceResolver services);
    public delegate TService ContextualNopBindingImpl<out TService, in TContext>(TContext context);

    public delegate TService PureNopBindingImpl<out TService>();
}