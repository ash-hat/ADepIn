# Atlas
A simple inversion of control container and small utilities used by it, made for frameworks.

## Notable Features
- Near-zero reflection
  - A small amount is used for entry module loading
  - A miniscule amount is used for nullable parameter checking, and only once per type (independent of amount of checks)
  - Absolutely no amount of `System.Reflection.Emit`
- No `NullReferenceException`s
  - All generic reference types are non-nullable using the `notnull` generic constraint
  - All reference and generic parameters are null-checked without boxing
  - `Option<T>` is used in place of traditional nullable reference types
- Module system to load other assemblies
  - `IModule` for manually constructed and loaded
  - `IEntryModule<TSelf>` to be discoverable by loaders
- Heavy emphasis on interfaces with extension methods
  - There is little to write for your own interface implementations
  - Allows you to write your own fluent bindings easily
- Publicly accessible utilies
  - `Guard`: API helper class, asserts arguments
  - `Option<T>`: discriminated union, API based off of Rust's [`std::option`](https://doc.rust-lang.org/std/option/index.html)
