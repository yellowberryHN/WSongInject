using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WSongInject
{
    internal class Song
    {
        public UInt32 UniqueID = 0;
        public string MusicMessage = "-";
        public string ArtistMessage = "-";
        public string CopyrightMessage = "-";
        public UInt32 VersionNo = 0;
        public string AssetDirectory = "S00-000";
        public string MovieAssetName = "-";
        public string MovieAssetNameHard = null;
        public string MovieAssetNameExpert = null;
        public string MovieAssetNameInferno = null;
        public string JacketAssetName = "S00/uT_J_S00_000";
        public string Rubi = "";

        public bool bValidCulture_ja_JP = true;
        public bool bValidCulture_en_US = true;
        public bool bValidCulture_zh_Hant_TW = true;
        public bool bValidCulture_en_HK = true;
        public bool bValidCulture_en_SG = true;
        public bool bValidCulture_ko_KR = true;
        public bool bValidCulture_h_Hans_CN_Guest = false;
        public bool bValidCulture_h_Hans_CN_GeneralMember = false;
        public bool bValidCulture_h_Hans_CN_VipMember = false;
        public bool bValidCulture_Offline = true;
        public bool bValidCulture_NoneActive = false;

        public bool bRecommend = false;

        public int WaccaPointCost = 0;

        public bool bCollaboration = false;
        public bool bWaccaOriginal = false;
        public byte TrainingLevel = 0;
        public byte Reserved = 0;

        public string Bpm = "-";
        public string HashTag = string.Empty;

        public string NotesDesignerNormal = "-";
        public string NotesDesignerHard = "-";
        public string NotesDesignerExpert = "-";
        public string NotesDesignerInferno = "-";

        public float DifficultyNormalLv;
        public float DifficultyHardLv;
        public float DifficultyExtremeLv;
        public float DifficultyInfernoLv;

        public float ClearNormaRateNormal;
        public float ClearNormaRateHard;
        public float ClearNormaRateExtreme;
        public float ClearNormaRateInferno;

        public float PreviewBeginTime;
        public float PreviewSeconds = 10f;

        public int ScoreGenre = -1;

        public int MusicTagForUnlock0;
        public int MusicTagForUnlock1;
        public int MusicTagForUnlock2;
        public int MusicTagForUnlock3;
        public int MusicTagForUnlock4;
        public int MusicTagForUnlock5;
        public int MusicTagForUnlock6;
        public int MusicTagForUnlock7;
        public int MusicTagForUnlock8;
        public int MusicTagForUnlock9;

        public UInt64 WorkBuffer;

        public string AssetFullPath = "D:/project/Mercury/Mercury/Content//MusicData/S00-000";

        public Song()
        {
        }

        public Song(uint uniqueID)
        {
            setID(uniqueID);
        }

        public void setID(uint id)
        {
            if (id > 99999) throw new ArgumentOutOfRangeException("Song ID cannot be above 99999.");

            UniqueID = id;

            var a = Regex.Match(UniqueID.ToString("D5"), @"(\d{2})(\d{3})");
            AssetDirectory = string.Format("S{0}-{1}", a.Groups[1], a.Groups[2]);
            JacketAssetName = string.Format("S{0}/uT_J_S{0}_{1}", a.Groups[1], a.Groups[2]);
        }
    }


}
