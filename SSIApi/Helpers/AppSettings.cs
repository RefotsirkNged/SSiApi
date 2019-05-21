using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Helpers
{
  /// <summary>
  /// Read the settings from the appsettings json file, and store them here
  /// </summary>
  public class AppSettings
  {
    public string Secret
    {
      get; set;
    }
  }
}