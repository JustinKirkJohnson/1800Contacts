using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Justin._1800Contacts.Logic.Interface;

namespace Justin._1800Contacts.Api.Controllers
{
	[RoutePrefix("api/classes")]
	public class ClassController : ApiController
	{

		private readonly IClassLogic _classLogic;

		public ClassController(IClassLogic classLogic)
		{
			_classLogic = classLogic;
		}

		[HttpPost]
		[Route("sortByPrerequisite")]
		public IEnumerable<string> SortByPrerequisite(List<string> classDependencyList)
		{
			return _classLogic.SortByPrerequisite(classDependencyList);
		}

	}
}
