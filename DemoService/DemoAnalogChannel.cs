using System.ComponentModel;
using AsAbstract;
using AsBasic;

namespace DemoService;

class DemoAnalogChannel: AnalogInput{
    class DemoAnalogInputSettings(DemoAnalogChannel channel): ISettings{
        [DisplayName("Id"), ReadOnly(true)]
        public string Id{
            get{
                return channel.FullId;
            }
        }
        [DisplayName("Name")]
        public string Name{
            get{
                return channel.Name;
            }
            set{
                channel.Name = value;
            }
        }
        [DisplayName("Status")]
        public bool Enabled{
            get{return channel.Enabled;}
            set{channel.Enabled = value;}
        }
        [DisplayName("Input")]
        public AnalogPort.InputMode Input{
            get{
                return (channel.IoPort as AnalogPort)!.Input;
            }
            set{
                (channel.IoPort as AnalogPort)!.Input = value;
            }
        }
        [DisplayName("Range")]
        public AnalogPort.RangeLevel Range{
            get{
                return (channel.IoPort as AnalogPort)!.Range;
            }
            set{
                (channel.IoPort as AnalogPort)!.Range = value;
            }
        }
        [DisplayName("Couple")]
        public AnalogPort.SignalMode Couple{
            get{
                return (channel.IoPort as AnalogPort)!.Couple;
            }
            set{
                (channel.IoPort as AnalogPort)!.Couple = value;
            }
        }
        [DisplayName("Gain")]
        public double Gain{
            get{
                return (channel.IoPort as AnalogPort)!.Gain;
            }
            set{
                (channel.IoPort as AnalogPort)!.Gain = value;
            }
        }
        [DisplayName("Offset")]
        public double Offset{
            get{
                return (channel.IoPort as AnalogPort)!.Offset;
            }
            set{
                (channel.IoPort as AnalogPort)!.Offset = value;
            }
        }
    }
 

    public override ISettings GetSettings()
    {
        return new DemoAnalogInputSettings(this);
    }
}