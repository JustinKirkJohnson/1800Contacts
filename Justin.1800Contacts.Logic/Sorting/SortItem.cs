namespace Justin._1800Contacts.Logic.Sorting
{

	public class SortItem : SortItem<SortItem>
	{
		public SortItem(string name) : base(name) { }
	}

	public class SortItem<T>
	{
		public string Name { get; private set; }
		public virtual T[] Dependencies { get; protected set; }

		public SortItem(string name)
		{
			Name = name;
		}

		public void SetDependencies(params T[] dependencies)
		{
			Dependencies = dependencies;
		}

		public override string ToString()
		{
			return Name;
		}
	}

}
