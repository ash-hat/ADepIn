# ADepIn ![makefile workflow status](https://img.shields.io/github/workflow/status/ash-hat/ADepIn/makefile/main?label=makefile&style=flat-square)
A dependency injector and small utilities used by it, made to be used by frameworks and their modules.

## Contents
1. [Examples](#examples)  
  1.1. [Getter calls](#getter-calls)  
  1.2. [Fluent binding calls](#fluent-binding-calls)  
  1.3. [Direct binding calls](#direct-binding-calls)  
2. [Notable Features](#notable-features)

## Examples
### Getter calls
```cs
using ADepIn;

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
using ADepIn;
using ADepIn.Impl; // for ADepIn.Impl.StandardServiceKernel only
using ADepIn.Fluent;

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
using ADepIn;
using ADepIn.Impl; // for ADepIn.Impl.StandardServiceKernel only

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
  - Rarely `NullReferenceException`s (see [Preventing NullReferenceExceptions](#preventing-nullreferenceexceptions))
- Near-zero reflection
  - Some is used for entry module loading
  - Absolutely no `System.Reflection.Emit` (SRE)
- Module system
  - `IModule` for modules that are manually constructed and loaded
  - `IEntryModule<TSelf>` for modules that are to be reflectively discovered and loaded
- Publicly accessible utilities
  - `Guard` is an API helper class that asserts arguments
  - `Option<T>` is a discriminated union implementation of an [option type](https://en.wikipedia.org/wiki/Option_type), heavily based off of Rust's [`std::option`](https://doc.rust-lang.org/std/option/index.html)
  - `Unit` is an empty readonly struct implementation of a [unit type](https://wikipedia.org/wiki/Unit_type)

### Preventing NullReferenceExceptions
`NullReferenceExceptions` are few and far between within ADepIn because of C# 8.0's non-nullable reference types, null guards on reference types, and support for `Option<T>`. However, it has a weak spot in generics. Without a boxing call or possible reflection call in every null check, it is impossible to guarantee a generic type is not `null`. Therefore, if ADepIn receives a `null` value within a non-null generic parameter, ADepIn will not throw an `ArgumentNullException`.  
For example:
```cs
using ADepIn;
using ADepIn.Impl;
using ADepIn.Fluent;

IServiceKernel kernel = new StandardServiceKernel();

kernel.Bind<IMyService>()
  .ToConstant(null); // null is being passed as TValue (IMyService), no null check is performed

...

// Neither of these throw because the kernel has no problem finding the binding and the binding returns Some
Option<IMyService> implOpt = kernel.Get<IMyService>();
IMyService impl = implOpt.Unwrap();

// Throws NullReferenceException
impl.DoThing();
```

The first (and easiest) approach is to target `netcoreapp3.0` if your project permits it. Not to release the binaries, but to utilize the nullable static analysis.  
The second way to combat this is to never use the `null` keyword. Get into the habit of using `Option<T>`, which explicitly states that there might not be a value. Be aware that code you call might return a `null` value still.
