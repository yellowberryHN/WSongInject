using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UAssetAPI;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace WSongInject
{
    public partial class MainForm : Form
    {
        public string WaccaDir = string.Empty;

        public UAsset mpt;
        public UAsset umt;
        public UAsset uit;

        public MainForm()
        {
            InitializeComponent();

            validCulturesListBox.SetItemChecked(0, true);
            validCulturesListBox.SetItemChecked(1, true);
            validCulturesListBox.SetItemChecked(2, true);
            validCulturesListBox.SetItemChecked(3, true);
            validCulturesListBox.SetItemChecked(4, true);
            validCulturesListBox.SetItemChecked(5, true);

            validCulturesListBox.SetItemChecked(9, true);

            // this is well beyond hacky
            difficultyInfernoLvUpDown_ValueChanged(difficultyInfernoLvUpDown, new EventArgs());
        }

        private StructPropertyData CreateSongData(uint id)
        {
            if(id > 99999) throw new ArgumentOutOfRangeException("Song ID cannot be above 99999.");

            var a = Regex.Match(id.ToString("D5"), @"(\d{2})(\d{3})");
            var assetId = string.Format("S{0}-{1}", a.Groups[1], a.Groups[2]);
            var jacketName = string.Format("S{0}/uT_J_S{0}_{1}", a.Groups[1], a.Groups[2]); // 10001: S10/uT_J_S10_001

            var newSongData = new StructPropertyData(new FName(id.ToString()))
            {
                StructType = new FName("MusicParameterTableData"),
                Value = new List<PropertyData>
                {
                    new UInt32PropertyData(new FName("UniqueID")) { Value = id },
                    new StrPropertyData(new FName("MusicMessage")) { Value = new FString("-") },
                    new StrPropertyData(new FName("ArtistMessage")) { Value = new FString("-") },
                    new StrPropertyData(new FName("CopyrightMessage")) { Value = new FString("-") },
                    new UInt32PropertyData(new FName("VersionNo")) { Value = 0 },
                    new StrPropertyData(new FName("AssetDirectory")) { Value = new FString(assetId) },
                    new StrPropertyData(new FName("MovieAssetName")) { Value = new FString("-") },
                    new StrPropertyData(new FName("MovieAssetNameHard")) { Value = null },
                    new StrPropertyData(new FName("MovieAssetNameExpert")) { Value = null },
                    new StrPropertyData(new FName("MovieAssetNameInferno")) { Value = null },
                    new StrPropertyData(new FName("JacketAssetName")) { Value = new FString(jacketName)},
                    new StrPropertyData(new FName("Rubi")) { Value = new FString("-") },

                    new BoolPropertyData(new FName("bValidCulture_ja_JP")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_en_US")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_zh_Hant_TW")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_en_HK")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_en_SG")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_ko_KR")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_h_Hans_CN_Guest")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_h_Hans_CN_GeneralMember")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_h_Hans_CN_VipMember")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_Offline")) { Value = false },
                    new BoolPropertyData(new FName("bValidCulture_NoneActive")) { Value = false },

                    new BoolPropertyData(new FName("bRecommend")) { Value = false },
                    new IntPropertyData(new FName("WaccaPointCost")) { Value = 0 },

                    new BytePropertyData(new FName("bCollaboration")) { Value = 0, EnumType = new FName("None") },
                    new BytePropertyData(new FName("bWaccaOriginal")) { Value = 0, EnumType = new FName("None") },
                    new BytePropertyData(new FName("TrainingLevel")) { Value = 0, EnumType = new FName("None") },
                    new BytePropertyData(new FName("Reserved")) { Value = 0, EnumType = new FName("None") },

                    new StrPropertyData(new FName("Bpm")) { Value = new FString("-") },
                    new StrPropertyData(new FName("HashTag")) { Value = new FString(assetId + "_CUSTOM") },

                    new StrPropertyData(new FName("NotesDesignerNormal")) { Value = new FString("-") },
                    new StrPropertyData(new FName("NotesDesignerHard")) { Value = new FString("-") },
                    new StrPropertyData(new FName("NotesDesignerExpert")) { Value = new FString("-") },
                    new StrPropertyData(new FName("NotesDesignerInferno")) { Value = new FString("-") },

                    new FloatPropertyData(new FName("DifficultyNormalLv")) { Value = 0 },
                    new FloatPropertyData(new FName("DifficultyHardLv")) { Value = 0 },
                    new FloatPropertyData(new FName("DifficultyExtremeLv")) { Value = 0 },
                    new FloatPropertyData(new FName("DifficultyInfernoLv")) { Value = 0 },

                    new FloatPropertyData(new FName("ClearNormaRateNormal")) { Value = 0.45f },
                    new FloatPropertyData(new FName("ClearNormaRateHard")) { Value = 0.55f },
                    new FloatPropertyData(new FName("ClearNormaRateExtreme")) { Value = 0.8f },
                    new FloatPropertyData(new FName("ClearNormaRateInferno")) { Value = 0.8f },

                    new FloatPropertyData(new FName("PreviewBeginTime")) { Value = 0f },
                    new FloatPropertyData(new FName("PreviewSeconds")) { Value = 10f },

                    new IntPropertyData(new FName("ScoreGenre")) { Value = 0 },

                    new IntPropertyData(new FName("MusicTagForUnlock0")) { Value = 4 },
                    new IntPropertyData(new FName("MusicTagForUnlock1")) { Value = 0 },
                    new IntPropertyData(new FName("MusicTagForUnlock2")) { Value = 0 },
                    new IntPropertyData(new FName("MusicTagForUnlock3")) { Value = 0 },
                    new IntPropertyData(new FName("MusicTagForUnlock4")) { Value = 0 },
                    new IntPropertyData(new FName("MusicTagForUnlock5")) { Value = 0 },
                    new IntPropertyData(new FName("MusicTagForUnlock6")) { Value = 0 },
                    new IntPropertyData(new FName("MusicTagForUnlock7")) { Value = 0 },
                    new IntPropertyData(new FName("MusicTagForUnlock8")) { Value = 0 },
                    new IntPropertyData(new FName("MusicTagForUnlock9")) { Value = 0 },

                    new UInt64PropertyData(new FName("WorkBuffer")) { Value = 0 },
                    new StrPropertyData(new FName("AssetFullPath")) { Value = new FString("D:/project/Mercury/Mercury/Content//MusicData/"+assetId) },
                }
            };

            return newSongData;
        }

        private StructPropertyData CreateUnlockData(uint id)
        {
            if (id > 99999) throw new ArgumentOutOfRangeException("Song ID cannot be above 99999.");

            var newUnlockData = new StructPropertyData(new FName("Data_" + id.ToString()))
            {
                StructType = new FName("UnlockMusicTableData"),
                Value = new List<PropertyData>
                {
                    new IntPropertyData(new FName("MusicId")) { Value = (int)id },
                    new Int64PropertyData(new FName("AdaptStartTime")) { Value = 0 },
                    new Int64PropertyData(new FName("AdaptEndTime")) { Value = 0 },
                    new BoolPropertyData(new FName("bRequirePurchase")) { Value = false },
                    new IntPropertyData(new FName("RequiredMusicOpenWaccaPoint")) { Value = 0 },
                    new BoolPropertyData(new FName("bVipPreOpen")) { Value = true },
                    new StrPropertyData(new FName("NameTag")) { Value = new FString("song name goes here") },
                    new StrPropertyData(new FName("ExplanationTextTag")) { Value = null },
                    new Int64PropertyData(new FName("ItemActivateStartTime")) { Value = 0 },
                    new Int64PropertyData(new FName("ItemActivateEndTime")) { Value = 0 },
                    new BoolPropertyData(new FName("bIsInitItem")) { Value = true },
                    new IntPropertyData(new FName("GainWaccaPoint")) { Value = 0 },
                }
            };

            return newUnlockData;
        }

        private StructPropertyData CreateInfernoData(uint id)
        {
            if (id > 99999) throw new ArgumentOutOfRangeException("Song ID cannot be above 99999.");

            var newInfernoData = new StructPropertyData(new FName(id.ToString()))
            {
                StructType = new FName("UnlockInfernoTableData"),
                Value = new List<PropertyData>
                {
                    new IntPropertyData(new FName("MusicId")) { Value = (int)id },
                    new BoolPropertyData(new FName("bRequirePurchase")) { Value = false },
                    new IntPropertyData(new FName("RequiredInfernoOpenWaccaPoint")) { Value = 0 },
                    new BoolPropertyData(new FName("bVipPreOpen")) { Value = true },
                    new StrPropertyData(new FName("NameTag")) { Value = new FString("song name goes here") },
                    new StrPropertyData(new FName("ExplanationTextTag")) { Value = new FString("TEXT(\"\")") },
                    new Int64PropertyData(new FName("ItemActivateStartTime")) { Value = 0 },
                    new Int64PropertyData(new FName("ItemActivateEndTime")) { Value = 0 },
                    new BoolPropertyData(new FName("bIsInitItem")) { Value = true },
                    new IntPropertyData(new FName("GainWaccaPoint")) { Value = 0 },
                }
            };

            return newInfernoData;
        }

        private void LoadMPT()
        {
            mpt = new UAsset(Path.Combine(WaccaDir, "WindowsNoEditor/Mercury/Content/Table/MusicParameterTable.uasset"), UE4Version.VER_UE4_19);
            Console.WriteLine("MPT data preserved: " + (mpt.VerifyBinaryEquality() ? "YES" : "NO"));
            
            /*
            Export baseUs = mpt.Exports[0];
            if (baseUs is DataTableExport us)
            {
                for (int j = 0; j < us.Table.Data.Count; j++)
                {
                    PropertyData me = us.Table.Data[j];
                    if (int.TryParse(me.Name.ToString(), out int _) && me is StructPropertyData struc)
                    {
                        IDictionary<string, PropertyData> w = new Dictionary<string, PropertyData>();

                        IList<PropertyData> cosa = struc.Value;
                        for (int m = 0; m < cosa.Count; m++)
                        {
                            PropertyData algo = cosa[m];

                            w.Add(algo.Name.ToString(), algo);
                        }
                        //if (((IntPropertyData)w["ScoreGenre"]).Value == 6)Console.WriteLine($"{w["MusicMessage"]}: {w["ScoreGenre"]}");
                    }
                }
            }
            */
        }

        private void LoadUMT()
        {
            umt = new UAsset(Path.Combine(WaccaDir, "WindowsNoEditor/Mercury/Content/Table/UnlockMusicTable.uasset"), UE4Version.VER_UE4_19);
            Console.WriteLine("UMT data preserved: " + (umt.VerifyBinaryEquality() ? "YES" : "NO"));

            /*
            Export baseUs = umt.Exports[0];
            if (baseUs is DataTableExport us)
            {
                for (int j = 0; j < us.Table.Data.Count; j++)
                {
                    PropertyData me = us.Table.Data[j];
                    if (me is StructPropertyData struc)
                    {
                        IDictionary<string, PropertyData> w = new Dictionary<string, PropertyData>();

                        IList<PropertyData> cosa = struc.Value;
                        for (int m = 0; m < cosa.Count; m++)
                        {
                            PropertyData algo = cosa[m];

                            w.Add(algo.Name.ToString(), algo);
                        }

                        //Console.WriteLine($"{w["MusicId"]}: {w["GainWaccaPoint"]}");
                    }
                }
            }
            */
        }

        private void LoadUIT()
        {
            uit = new UAsset(Path.Combine(WaccaDir, "WindowsNoEditor/Mercury/Content/Table/UnlockInfernoTable.uasset"), UE4Version.VER_UE4_19);
            Console.WriteLine("UIT data preserved: " + (umt.VerifyBinaryEquality() ? "YES" : "NO"));
        }

        private void reloadData()
        {
            WaccaDir = waccaDirBox.Text;
            LoadMPT();
            LoadUMT();
            LoadUIT();
            if (mpt != null && umt != null && uit != null)
            {
                reloadBtn.Text = "Reload";
                uniqueIDSelect_ValueChanged(uniqueIDSelect, new EventArgs()); // trigger a value change without a change
                songTabs.Visible = true;
                injectBtn.Enabled = true;
            }  
        }

        private void injectBtn_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to inject this song?", "WACCA Song Injector", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (mpt.ContainsNameReference(FString.FromString(uniqueIDSelect.Value.ToString())))
                    MessageBox.Show($"Error: can't inject a song with ID {uniqueIDSelect.Value} as one already exists!", "WACCA Song Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else inject();
            }
        }

        private void inject()
        {
            if (mpt.Exports[0] is DataTableExport dt1)
            {
                // this will use the Song object later, I just don't have time to figure it out right now.
                var newSong = CreateSongData((uint)uniqueIDSelect.Value);

                IList<PropertyData> propList = newSong.Value;

                ((StrPropertyData)propList[1]).Value = new FString(musicMessageBox.Text);
                ((StrPropertyData)propList[2]).Value = new FString(artistMessageBox.Text);
                ((StrPropertyData)propList[3]).Value = new FString(copyrightMessageBox.Text);
                ((UInt32PropertyData)propList[4]).Value = (uint)versionNoUpDown.Value;
                ((StrPropertyData)propList[11]).Value = new FString(rubiBox.Text);

                foreach (int culture in validCulturesListBox.CheckedIndices)
                {
                    ((BoolPropertyData)propList[12 + culture]).Value = true;
                }

                ((BoolPropertyData)propList[23]).Value = bRecommendChk.Checked;
                ((IntPropertyData)propList[24]).Value = (int)waccaPointCostUpDown.Value;

                ((BytePropertyData)propList[25]).Value = (byte)(bCollaborationChk.Checked ? 1 : 0);
                ((BytePropertyData)propList[26]).Value = (byte)(bWaccaOriginalChk.Checked ? 1 : 0);
                ((BytePropertyData)propList[27]).Value = (byte)trainingLevelUpDown.Value;

                ((StrPropertyData)propList[29]).Value = new FString(bpmBox.Text);

                ((StrPropertyData)propList[31]).Value = new FString(notesDesignerNormalBox.Text);
                ((StrPropertyData)propList[32]).Value = new FString(notesDesignerHardBox.Text);
                ((StrPropertyData)propList[33]).Value = new FString(notesDesignerExpertBox.Text);
                ((StrPropertyData)propList[34]).Value = new FString(notesDesignerInfernoBox.Text);

                ((FloatPropertyData)propList[35]).Value = (float)difficultyNormalLvUpDown.Value;
                ((FloatPropertyData)propList[36]).Value = (float)difficultyHardLvUpDown.Value;
                ((FloatPropertyData)propList[37]).Value = (float)difficultyExtremeLvUpDown.Value;
                ((FloatPropertyData)propList[38]).Value = (float)difficultyInfernoLvUpDown.Value;

                ((FloatPropertyData)propList[39]).Value = (float)clearNormaRateNormalUpDown.Value;
                ((FloatPropertyData)propList[40]).Value = (float)clearNormaRateHardUpDown.Value;
                ((FloatPropertyData)propList[41]).Value = (float)clearNormaRateExtremeUpDown.Value;
                ((FloatPropertyData)propList[42]).Value = (float)clearNormaRateInfernoUpDown.Value;

                ((FloatPropertyData)propList[43]).Value = (float)previewBeginTimeUpDown.Value;
                ((FloatPropertyData)propList[44]).Value = (float)previewSecondsUpDown.Value;

                ((IntPropertyData)propList[45]).Value = scoreGenreComboBox.SelectedIndex;

                ((IntPropertyData)propList[46]).Value = (int)MusicTagForUnlock0UpDown.Value;
                ((IntPropertyData)propList[47]).Value = (int)MusicTagForUnlock1UpDown.Value;
                ((IntPropertyData)propList[48]).Value = (int)MusicTagForUnlock2UpDown.Value;
                ((IntPropertyData)propList[49]).Value = (int)MusicTagForUnlock3UpDown.Value;
                ((IntPropertyData)propList[50]).Value = (int)MusicTagForUnlock4UpDown.Value;
                ((IntPropertyData)propList[51]).Value = (int)MusicTagForUnlock5UpDown.Value;
                ((IntPropertyData)propList[52]).Value = (int)MusicTagForUnlock6UpDown.Value;
                ((IntPropertyData)propList[53]).Value = (int)MusicTagForUnlock7UpDown.Value;
                ((IntPropertyData)propList[54]).Value = (int)MusicTagForUnlock8UpDown.Value;
                ((IntPropertyData)propList[55]).Value = (int)MusicTagForUnlock9UpDown.Value;

                mpt.AddNameReference(FString.FromString(uniqueIDSelect.Value.ToString()));
                dt1.Table.Data.Add(newSong);
                if (umt.Exports[0] is DataTableExport dt2)
                {
                    var unlock = CreateUnlockData((uint)uniqueIDSelect.Value);

                    IList<PropertyData> propList2 = unlock.Value;

                    ((Int64PropertyData)propList2[1]).Value = (long)adaptStartTimeUpDown.Value;
                    ((Int64PropertyData)propList2[2]).Value = (long)adaptEndTimeUpDown.Value;
                    ((BoolPropertyData)propList2[3]).Value = bRequirePurchaseChk.Checked;
                    ((IntPropertyData)propList2[4]).Value = (int)requiredMusicOpenWaccaPointUpDown.Value;
                    ((BoolPropertyData)propList2[5]).Value = bVipPreOpenChk.Checked;
                    ((StrPropertyData)propList2[6]).Value = new FString(musicMessageBox.Text);

                    ((Int64PropertyData)propList2[8]).Value = (long)itemActivateStartTimeUpDown.Value;
                    ((Int64PropertyData)propList2[9]).Value = (long)itemActivateEndTimeUpDown.Value;

                    ((BoolPropertyData)propList2[10]).Value = bIsInitItemChk.Checked;

                    ((IntPropertyData)propList2[11]).Value = (int)gainWaccaPointUpDown.Value;

                    umt.AddNameReference(FString.FromString("Data_" + uniqueIDSelect.Value.ToString()));
                    dt2.Table.Data.Add(unlock);

                    if (uit.Exports[0] is DataTableExport dt3 && difficultyInfernoLvUpDown.Value != decimal.Zero)
                    {
                        var unlock_inf = CreateInfernoData((uint)uniqueIDSelect.Value);

                        IList<PropertyData> propList3 = unlock_inf.Value;

                        ((BoolPropertyData)propList3[1]).Value = inf_bRequirePurchaseChk.Checked;
                        ((IntPropertyData)propList3[2]).Value = (int)requiredInfernoOpenWaccaPointUpDown.Value;
                        ((BoolPropertyData)propList3[3]).Value = inf_bVipPreOpenChk.Checked;
                        ((StrPropertyData)propList3[4]).Value = new FString(musicMessageBox.Text);

                        ((Int64PropertyData)propList3[6]).Value = (long)itemActivateStartTimeUpDown.Value;
                        ((Int64PropertyData)propList3[7]).Value = (long)itemActivateEndTimeUpDown.Value;

                        ((BoolPropertyData)propList3[8]).Value = bIsInitItemChk.Checked;

                        ((IntPropertyData)propList3[9]).Value = (int)gainWaccaPointUpDown.Value;

                        uit.AddNameReference(FString.FromString(uniqueIDSelect.Value.ToString()));
                        dt3.Table.Data.Add(unlock_inf);

                        mpt.Write(mpt.FilePath);
                        umt.Write(umt.FilePath);
                        uit.Write(uit.FilePath);
                    } 
                    else
                    {
                        // write without inferno data
                        mpt.Write(mpt.FilePath);
                        umt.Write(umt.FilePath);
                    }
                }
            }

            MessageBox.Show("written, reloading data...");
            reloadData();
        }

        private void reloadBtn_Click(object sender, EventArgs e)
        {
            reloadData();
        }

        private void uniqueIDSelect_ValueChanged(object sender, EventArgs e)
        {
            if (mpt.ContainsNameReference(FString.FromString(uniqueIDSelect.Value.ToString())))
            {
                uniqueIDSelect.ForeColor = Color.Red;
            } 
            else
            {
                uniqueIDSelect.ForeColor = SystemColors.WindowText;
            }

            var a = Regex.Match(((int)uniqueIDSelect.Value).ToString("D5"), @"(\d{2})(\d{3})");
            assetDirectoryBox.Text = string.Format("S{0}-{1}", a.Groups[1], a.Groups[2]);
            jacketAssetNameBox.Text = string.Format("S{0}/uT_J_S{0}_{1}", a.Groups[1], a.Groups[2]);
        }

        private void musicMessageBox_TextChanged(object sender, EventArgs e)
        {
            rubiBox.Text = musicMessageBox.Text;
        }

        private void rubiBox_Leave(object sender, EventArgs e)
        {
            if (rubiBox.Text == String.Empty) rubiBox.Text = musicMessageBox.Text;
        }

        private void validCulturesListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(validCulturesListBox.SelectedIndices.ToString());
        }

        private void openFolderBtn_Click(object sender, EventArgs e)
        {
            using (var ofd = new CommonOpenFileDialog { IsFolderPicker = true, InitialDirectory = waccaDirBox.Text })
            {
                if(ofd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    if (!Directory.Exists(Path.Combine(ofd.FileName, @"WindowsNoEditor\Mercury\Content")))
                    {
                        MessageBox.Show("This doesn't appear to be a valid root directory.\n\nPlease choose the directory that contains WindowsNoEditor and bin.");
                    }
                    else
                    {
                        waccaDirBox.Text = ofd.FileName;
                        reloadData();
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            wpCostLabel2.Enabled = requiredMusicOpenWaccaPointUpDown.Enabled = bRequirePurchaseChk.Checked;
        }

        private void waccaPointCostUpDown_ValueChanged(object sender, EventArgs e)
        {
            requiredMusicOpenWaccaPointUpDown.Value = waccaPointCostUpDown.Value;
        }

        private void difficultyLvUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (((DifficultyNUD)sender).Value > decimal.Zero && ((DifficultyNUD)sender).Value < decimal.One)
            {
                ((DifficultyNUD)sender).Value = decimal.One;
            }
        }

        private void difficultyInfernoLvUpDown_ValueChanged(object sender, EventArgs e)
        {
            difficultyLvUpDown_ValueChanged(sender, e);
            if (difficultyInfernoLvUpDown.Value >= decimal.One)
            {
                infernoGroupBox.Visible = true;

                infernoClearLabel.Enabled = infernoLabel.Enabled = infernoNDLabel.Enabled =
                    clearNormaRateInfernoUpDown.Enabled = notesDesignerInfernoBox.Enabled = true;
            }
            else
            {
                infernoGroupBox.Visible = false;

                infernoClearLabel.Enabled = infernoLabel.Enabled = infernoNDLabel.Enabled =
                    clearNormaRateInfernoUpDown.Enabled = notesDesignerInfernoBox.Enabled = false;
            }
        }

        private void inf_bRequirePurchaseChk_CheckedChanged(object sender, EventArgs e)
        {
            inf_wpCostLabel.Enabled = requiredInfernoOpenWaccaPointUpDown.Enabled = inf_bRequirePurchaseChk.Checked;
        }

        private string jacketHeader = "AAAAAAEAAQABAAAADwAAAAAAAADmBBAAAAIAAAACAAABAAAADAAAAFBGX0I4RzhSOEE4AAAAAAABAAAAAQAAAEgAAAAAABAAAAAQAN4EAAAAAAAA";
        private string jacketFooter = "AAIAAAACAAANAAAAAAAAAA==";

        private void makeJacket()
        {
            using (var ofd = new CommonOpenFileDialog { Title = "Select a jacket image"})
            {
                ofd.Filters.Add(new CommonFileDialogFilter("Jacket Image (512x512)", "*.png;*.bmp;*.jpg;*.jpeg"));
                if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var bmp = new Bitmap(ofd.FileName);
                    if(bmp.Size != new Size(512,512))
                    {
                        MessageBox.Show($"Error: Jacket size must be exactly 512x512 pixels!", "WACCA Song Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bmp = bmp.Clone(new Rectangle(0, 0, 512, 512), PixelFormat.Format32bppArgb);

                    var asset = UAsset.DeserializeJson(new MemoryStream(WSongInject.Properties.Resources.jacketJson));

                    if (asset.Exports[0] is NormalExport jacket)
                    {
                        jacket.ObjectName = new FName(jacketAssetNameBox.Text.Substring(3));

                        using (var ms = new MemoryStream())
                        using (var writer = new BinaryWriter(ms))
                        {
                            writer.Write(Convert.FromBase64String(jacketHeader));
                            for (int y = 0; y < 512; y++)
                            {
                                for (int x = 0; x < 512; x++)
                                {
                                    // winforms fucking sucks so I had to do this.
                                    // what a terrible, slow piece of code
                                    var c = bmp.GetPixel(x, y);
                                    writer.Write(new byte[4] { c.B, c.G, c.R, c.A });
                                }
                            }
                            writer.Write(Convert.FromBase64String(jacketFooter));
                            jacket.Extras = ms.ToArray();
                        }

                        asset.SetNameReference(asset.SearchNameReference(new FString("uT_J_S00_000")), new FString(jacketAssetNameBox.Text.Substring(3)));
                        asset.SetNameReference(asset.SearchNameReference(
                            new FString("/Game/UI/Textures/JACKET/S00/uT_J_S00_000")),
                            new FString($"/Game/UI/Textures/JACKET/{jacketAssetNameBox.Text}")
                        );

                        Directory.CreateDirectory(Path.Combine(WaccaDir, $"WindowsNoEditor/Mercury/Content/UI/Textures/JACKET/{jacketAssetNameBox.Text.Substring(0,3)}"));
                        asset.Write(Path.Combine(WaccaDir, $"WindowsNoEditor/Mercury/Content/UI/Textures/JACKET/{jacketAssetNameBox.Text}.uasset"));
                    }
                }
            }
        }

        private void createJacketBtn_Click(object sender, EventArgs e)
        {
            makeJacket();
        }
    }
}
