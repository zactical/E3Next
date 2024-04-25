﻿using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Core.Data
{
    public class MelodyIfs
    {
        public String MelodyName;
        public String MelodyIf;
        public MelodyIfs()
        {

        }
        public MelodyIfs(string melodyName, IniData parsedData)
        {
            string[] melodyArray = melodyName.Split('/');

            MelodyName = melodyArray[0];

            if(melodyArray.Length>1)
            {
                string ifKey = Spell.GetArgument<string>(melodyArray[1]);
                var section = parsedData.Sections["Ifs"];
                if (section != null)
                {
                    var keyData = section[ifKey];
                    if (!String.IsNullOrWhiteSpace(keyData))
                    {
                        MelodyIf = keyData;
                    }
                }
            }
        }
    }
}
