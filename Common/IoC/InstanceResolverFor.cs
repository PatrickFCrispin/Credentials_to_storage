using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.IoC
{
	public static class InstanceResolverFor
	{
		public static IDictionary<string, string> InstanceBuilderDefinitions { get; } = new Dictionary<string, string>();
	}

	public static class InstanceResolverFor<TContract>
	{
		public static Func<TContract> InstanceBuilder { get; set; }

		public static TContract Instance { get { return InstanceBuilder(); } }

        public static void Configure(Expression<Func<TContract>> instance)
		{
			InstanceResolverFor.InstanceBuilderDefinitions[typeof(TContract).Name] = instance.ToString();

			InstanceBuilder = instance.Compile();
		}

		public static void BindSingletonOf<TInstance>() where TInstance : class, TContract, new()
		{
			InstanceResolverFor.InstanceBuilderDefinitions[typeof(TContract).Name] = $"SingletonFor<{typeof(TInstance).FullName}>";

			InstanceBuilder = () => SingletonFor<TInstance>.Instance;
		}

		public static void BindInstanceOf<TInstance>() where TInstance : class, TContract, new()
		{
			InstanceResolverFor.InstanceBuilderDefinitions[typeof(TContract).Name] = $"InstanceOf<{typeof(TInstance).FullName}>";

			InstanceBuilder = () => new TInstance();
		}
	}
}