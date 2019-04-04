using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nodeManager
{
    public enum ProgramLanguage { Python, JavaScript }

    public class DockerImage
    {
        public ProgramLanguage ProgramLanguage = ProgramLanguage.Python;
        public string PathToProgram = "";
    }
}