// Copied and modified from InventoryKamera(https://github.com/Andrewthe13th/Inventory_Kamera) under MIT License.

using ArtifactSelector.model;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArtifactSelector.Scanner
{
    internal class ArtifactScanner
    {
        private Navigation nav;
        private int row = 0;
        private int column = -1;
        private int page = 0;
        private int clickDelay = 200;
        private int scrollDelay = 100;
        public ArtifactScanner()
        {
            nav = new Navigation();
        }

        public void DeselectArtifact()
        {
            nav.MoveCenterToDeselect();
            nav.Click();
            nav.Wait(clickDelay);
            nav.MoveDeselectToCenter();
        }

        public void LockArtifact()
        {
            nav.SavePosition();
            nav.MoveToLockButton();
            nav.Click();
            nav.Wait(clickDelay);
            nav.MoveToSavedPosition();
        }

        public Artifact Next()
        {
            // First Artifact
            if (column == -1)
            {
                column++;
                nav.MoveToFirstArtifact();
            }
            // Middle of row
            else if (column < 5)
            {
                column++;
                nav.MoveToRightArtifact();
            }
            // Reach end of row 
            else if (row < 4)
            {
                column = 0;
                row++;
                nav.MoveToLeftMostArtifact();
                nav.MoveToBottomArtifact();
            }
            // Reach end of page
            else
            {
                // Scroll less every 3 page to align
                if (page % 3 == 2)
                {
                    nav.Scroll(-46);
                }
                else
                {
                    nav.Scroll(-47);
                }
                nav.Wait(scrollDelay);

                column = 0;
                row = 0;
                page += 1;
                nav.MoveToFirstArtifact();
            }

            nav.Click();
            nav.Wait(clickDelay);

            Bitmap bitmap = nav.CaptureRegion(nav.ScaleToActualRect(Dimension.ARTI_CARD_RECT));
            Artifact artifact = ScanArtifact(bitmap);
            bitmap.Dispose();
            return artifact;
        }

        private Artifact ScanArtifact(Bitmap artifactImage)
        {
            GearSlot slot = ScanArtifactGearSlot(artifactImage);
            ArtifactSet set = ScanArtifactSet(artifactImage);
            MainStat mainStat = ScanArtifactMainStat(artifactImage);
            List<SubStat> subStats = ScanArtifactSubStats(artifactImage);

            if (slot == GearSlot.Flower)
            {
                mainStat = MainStat.Hp;
            }

            if (slot == GearSlot.Plume)
            {
                mainStat = MainStat.Atk;
            }

            return new Artifact(slot, set, mainStat, subStats);

        }

        private GearSlot ScanArtifactGearSlot(Bitmap artifactImage)
        {
            Bitmap gearSlotBm = BitmapProcessor.CopyBitmap(artifactImage, nav.ScaleToActualRect(Dimension.ARTI_SLOT_RECT));
            gearSlotBm = BitmapProcessor.ConvertToGrayscale(gearSlotBm);
            BitmapProcessor.SetContrast(80.0, ref gearSlotBm);
            BitmapProcessor.SetInvert(ref gearSlotBm);
            string slot = BitmapProcessor.AnalyzeText(gearSlotBm).Trim().ToLower().Split()[0];

            gearSlotBm.Dispose();

            return GearSlotDictionary.FromScanner(slot);
        }

        private MainStat ScanArtifactMainStat(Bitmap artifactImage)
        {
            Bitmap mainSlotBm = BitmapProcessor.CopyBitmap(artifactImage, nav.ScaleToActualRect(Dimension.ARTI_MAINSTAT_RECT));
            BitmapProcessor.SetContrast(100.0, ref mainSlotBm);
            mainSlotBm = BitmapProcessor.ConvertToGrayscale(mainSlotBm);
            BitmapProcessor.SetThreshold(135, ref mainSlotBm);
            BitmapProcessor.SetInvert(ref mainSlotBm);
            string mainStat = BitmapProcessor.AnalyzeText(mainSlotBm).Trim().ToLower().Replace(" ", "");

            mainSlotBm.Dispose();

            return MainStatDictionary.FromScanner(mainStat);
        }

        private List<SubStat> ScanArtifactSubStats(Bitmap artifactImage)
        {
            Bitmap substatBm = BitmapProcessor.CopyBitmap(artifactImage, nav.ScaleToActualRect(Dimension.ARTI_SUBSTAT_RECT));
            List<string> lines = new List<string>();
            string text;

            BitmapProcessor.SetBrightness(-30, ref substatBm);
            BitmapProcessor.SetContrast(85, ref substatBm);
            substatBm = BitmapProcessor.ConvertToGrayscale(substatBm);
            text = BitmapProcessor.AnalyzeText(substatBm, Tesseract.PageSegMode.Auto).ToLower().Replace(" ", "");

            substatBm.Dispose();

            lines = new List<string>(text.Split('\n'));
            lines.RemoveAll(line => string.IsNullOrWhiteSpace(line));

            var index = lines.FindIndex(line => line.Contains(":") || line.Contains("piece") || line.Contains("set") || !line.Any(char.IsDigit));
            if (index >= 0)
            {
                lines.RemoveRange(index, lines.Count - index);
            }

            List<SubStat> stats = new List<SubStat>();

            foreach (string line in lines)
            {
                List<string> l = line.Split('+').ToList();
                if (l.Count() < 2)
                {
                    stats.Add(SubStat.None);
                    continue;
                }

                string stat = l[0];
                stat = line.Contains("%") ? stat + "%" : stat;

                stats.Add(SubStatDictionary.FromScanner(stat));
            }

            return stats;
        }

        private ArtifactSet ScanArtifactSet(Bitmap artifactImage)
        {
            Bitmap setNameBm = BitmapProcessor.CopyBitmap(artifactImage, nav.ScaleToActualRect(Dimension.ARTI_SET_NAME_RECT));
            BitmapProcessor.SetGamma(0.2, 0.2, 0.2, ref setNameBm);
            setNameBm = BitmapProcessor.ConvertToGrayscale(setNameBm);
            BitmapProcessor.SetInvert(ref setNameBm);

            string text = BitmapProcessor.AnalyzeText(setNameBm, Tesseract.PageSegMode.Auto).ToLower();

            // setNameBm.Save(filedest, System.Drawing.Imaging.ImageFormat.Png);
            setNameBm.Dispose();

            List<string> lines = new List<string>(text.Split('\n'));
            lines.RemoveAll(line => string.IsNullOrWhiteSpace(line));

            if (lines.Count() < 2)
            {
                return ArtifactSet.None;
            }

            text = lines[0];
            if (text.Contains("+") || text.Contains("%"))
            {
                text = lines[1];
            }

            text = Regex.Replace(text, "[^a-z]", string.Empty);

            return ArtifactSetDictionary.FromScanner(text);
        }
    }
}
