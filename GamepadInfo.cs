using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputTest
{
  public class GamepadInfo
  {
    public Guid InstanceGuid { get; private set; }
    public string InstanceName;
    public Guid ProductGuid;
    public string ProductName;
    public string DeviceType;
    public int SubType;

    public GamepadInfo(Guid guid)
    {
      InstanceGuid = guid;
    }
  }
}
