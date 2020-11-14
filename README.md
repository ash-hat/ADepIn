# Atlas ![makefile workflow status](https://img.shields.io/github/workflow/status/ash-hat/Atlas/makefile/main?label=makefile&style=flat-square)
A (relatively) simple inversion of control container and small utilities used by it, made to be used by frameworks and their modules.

## Contents
1. [Examples](#examples)  
  1.1. [Getter calls](#getter-calls)  
  1.2. [Fluent binding calls](#fluent-binding-calls)  
  1.3. [Direct binding calls](#direct-binding-calls)  
2. [Notable Features](#notable-features)

## Examples
### Getter calls
```cs
using Atlas;

// Assign information to pass to the binding
IMyContext context = ...;

// Try to get implementations of the service given a context (#1) and given no context (#2)
Option<IMyService> implContextfulOpt = kernel.Get<IMyService, IMyContext>(context);
Option<IMyService> implContextlessOpt = kernel.Get<IMyService>();

// Gets the implementations
// These calls will throw if they do not exist
IMyService implContextful = implContextfulOpt.Unwrap();
IMyService implContextless = implContextlessOpt.Unwrap();

// Checks if the implementations exist, and if they do, does something
// Similar to Try* methods
if (implContextfulOpt.MatchSome(out IMyService implContextful))
{
  ...
}
if (implContextfulOpt.MatchSome(out IMyService implContextless))
{
  ...
}
```
### Fluent binding calls
Fluent is the easy and recommended way to bind. In fact, it even makes the bindings for you!

```cs
using Atlas;
using Atlas.Impl; // for Atlas.Impl.StandardServiceKernel only
using Atlas.Fluent;

// Create something to hold bindings (service implementations)
IServiceKernel kernel = new StandardServiceKernel();

// Creates a contextless binding stub for IMyService
// To make a contextful stub, simply use kernel.Bind<IMyService, IMyContext>()
kernel.Bind<IMyService>()
  // "To" tells the stub how to make the service
  // PureNopMethod takes no inputs and outputs just the service, no-option (hence Nop)
  .ToPureNopMethod(() => ...)
  // "In" applies the stub, and may apply final modifiers
  // Transient has no modifiers; it will run the "To" every time
  .InTransientScope();
```

### Direct binding calls
Direct binding is simple, yet inflexible. This is how the library fundamentally works.

```cs
using Atlas;
using Atlas.Impl; // for Atlas.Impl.StandardServiceKernel only

// Create something to hold bindings (service implementations)
IServiceKernel kernel = new StandardServiceKernel();

// Assign bindings which take a context (#1) and take no context (#2) to create a service
IServiceBinding<IMyService, IMyContext> bindingContextful = ...;
IServiceBinding<IMyService, Unit> bindingContextless = ...;

// Add the bindings to the kernel
kernel.Bind(bindingContextful);
kernel.Bind(bindingContextless);
```

## Notable Features
Why use yet another dependency injection library?

- Infrequent exceptions
  - Some `Argument...Exception`s, largely from non-nullable public APIs
  - A few `InvalidOperationException`s
  - No `NullReferenceException`s, because of C# 8.0 `notnull` generic constraint, null guards, and `Option<T>`
- Near-zero reflection
  - Some is used for entry module loading
  - A little is used for nullable parameter checking, and is cached per type
  - Absolutely no `System.Reflection.Emit` (SRE) - Atlas was made primarily for game modding, and some runtimes of Unity do not support SRE. Many existing solutions require or have obtuse opt-out SRE for its reflective performance, but it is out of scope for Atlas.
- Module system
  - `IModule` for modules that are manually constructed and loaded
  - `IEntryModule<TSelf>` for modules that are to be reflectively discovered and loaded
- Publicly accessible utilities
  - `Nullability<T>` has type nullability and can check if an instance is null (don't get nullability and then call the check, it does that for you)
  - `Guard` is an API helper class that asserts arguments
  - `Option<T>` is a discriminated union implementation of an [option type](https://en.wikipedia.org/wiki/Option_type), heavily based off of Rust's [`std::option`](https://doc.rust-lang.org/std/option/index.html)
  - `Unit` is an empty readonly struct implementation of a [unit type](https://wikipedia.org/wiki/Unit_type)
