using System;
using System.Text.RegularExpressions;

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

        public MusicTag[] MusicTags = new MusicTag[10];

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

    public enum MusicTag
    {
        INVALID,
        JENRE_ORIGINAL,
        JENRE_JPOP,
        JENRE_ANIME,
        JENRE_VOCALOID,
        JENRE_25D,
        JENRE_ANIME_POP,
        JENRE_VARIETY,
        JENRE_TOHO,
        SCORE_ROTATION = 1000,
        SCORE_HIGH_SPEED,
        ARTIST_HARDCORE_TANOC = 10000,
        ARTIST_REDALICE,
        ARTIST_TPAZOLITE,
        ARTIST_USAO,
        ARTIST_PLIGHT,
        ARTIST_DJ_GENKI,
        ARTIST_DJ_NORIKEN,
        ARTIST_MASSIVE_NEW_KREW,
        ARTIST_DJ_MYOSUKE,
        ARTIST_KOBARYO,
        ARTIST_ARAN,
        ARTIST_MINAMOTOYA,
        ARTIST_KENTA_VEZ,
        ARTIST_NOIZENECIO,
        ARTIST_SRAV3R,
        ARTIST_GETTY,
        ARTIST_LAUR,
        ARTIST_GRAM,
        ARTIST_ARUFA = 10100,
        ARTIST_VTUBER,
        ARTIST_KIZUNA_AI,
        ARTIST_HOLOLIVE,
        ARTIST_COSMO,
        ARTIST_SAKUZYO,
        ARTIST_CAMELLIA,
        COLLAB_GROOVE_COASTER = 10200,
        COLLAB_BLEND_S,
        COLLAB_PRETTY_SERIES,
        COLLAB_DANMACHI,
        COLLAB_LANOTA,
        COLLAB_D4DJ,
        COLLAB_AZURLANE,
        COLLAB_ARCAEA,
        COLLAB_MUSEDASH,
        DIVE_WITH_U,
        BOSS_XTREME = 20000,
        BOSS_TENSHI
    }

}
