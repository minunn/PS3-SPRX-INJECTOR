using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PS3_SPRX_Loader
{
    class RPC
    {
        public static uint fxBirthTime = 0x90;
        public static uint fxDecayDuration = 0x9c;
        public static uint fxDecayStartTime = 0x98;
        public static uint fxLetterTime = 0x94;


        private static uint function_address = 0x38EDE8;

        public static int Call(UInt32 func_address, params object[] parameters)
        {
            int length = parameters.Length;
            int index = 0;
            UInt32 num3 = 0;
            UInt32 num4 = 0;
            UInt32 num5 = 0;
            UInt32 num6 = 0;
            while (index < length)
            {
                if (parameters[index] is int)
                {
                    Form1.PS3.Extension.WriteInt32(0x10050000 + (num3 * 4), (int)parameters[index]);
                    num3++;
                }
                else if (parameters[index] is UInt32)
                {
                    Form1.PS3.Extension.WriteUInt32(0x10050000 + (num3 * 4), (UInt32)parameters[index]);
                    num3++;
                }
                else
                {
                    UInt32 num7;
                    if (parameters[index] is string)
                    {
                        num7 = 0x10052000 + (num4 * 0x400);
                        Form1.PS3.Extension.WriteString(num7, Convert.ToString(parameters[index]));
                        Form1.PS3.Extension.WriteUInt32(0x10050000 + (num3 * 4), num7);
                        num3++;
                        num4++;
                    }
                    else if (parameters[index] is float)
                    {
                        Form1.PS3.Extension.WriteFloat(0x10050024 + (num5 * 4), (float)parameters[index]);
                        num5++;
                    }
                    else if (parameters[index] is float[])
                    {
                        float[] input = (float[])parameters[index];
                        num7 = 0x10051000 + (num6 * 4);
                        RPC.WriteSingle(num7, input);
                        Form1.PS3.Extension.WriteUInt32(0x10050000 + (num3 * 4), num7);
                        num3++;
                        num6 += (UInt32)input.Length;
                    }
                }
                index++;
            }
            Form1.PS3.Extension.WriteUInt32(0x1005004C, func_address);
            Thread.Sleep(20);
            return Form1.PS3.Extension.ReadInt32(0x10050050);
        }

        public static void EnableRPC()
        {
            byte[] RPC = { 0xF8, 0x21, 0xFF, 0x91, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x01, 0x00, 0x80, 0x3C, 0x40, 0x00, 0x72, 0x30, 0x42, 0x4C, 0x38, 0x3C, 0x60, 0x10, 0x05, 0x81, 0x83, 0x00, 0x4C, 0x2C, 0x0C, 0x00, 0x00, 0x41, 0x82, 0x00, 0x64, 0x80, 0x83, 0x00, 0x04, 0x80, 0xA3, 0x00, 0x08, 0x80, 0xC3, 0x00, 0x0C, 0x80, 0xE3, 0x00, 0x10, 0x81, 0x03, 0x00, 0x14, 0x81, 0x23, 0x00, 0x18, 0x81, 0x43, 0x00, 0x1C, 0x81, 0x63, 0x00, 0x20, 0xC0, 0x23, 0x00, 0x24, 0xC0, 0x43, 0x00, 0x28, 0xC0, 0x63, 0x00, 0x2C, 0xC0, 0x83, 0x00, 0x30, 0xC0, 0xA3, 0x00, 0x34, 0xC0, 0xC3, 0x00, 0x38, 0xC0, 0xE3, 0x00, 0x3C, 0xC1, 0x03, 0x00, 0x40, 0xC1, 0x23, 0x00, 0x48, 0x80, 0x63, 0x00, 0x00, 0x7D, 0x89, 0x03, 0xA6, 0x4E, 0x80, 0x04, 0x21, 0x3C, 0x80, 0x10, 0x05, 0x38, 0xA0, 0x00, 0x00, 0x90, 0xA4, 0x00, 0x4C, 0x90, 0x64, 0x00, 0x50, 0x3C, 0x40, 0x00, 0x73, 0x30, 0x42, 0x4B, 0xE8, 0xE8, 0x01, 0x00, 0x80, 0x7C, 0x08, 0x03, 0xA6, 0x38, 0x21, 0x00, 0x70, 0x4E, 0x80, 0x00, 0x20 };
            Form1.PS3.SetMemory(function_address, RPC);
            Form1.PS3.SetMemory(0x10050000, new byte[0x2854]);
        }
        public class HudStruct
        {
            public static uint
            xOffset = 0x08,
            yOffset = 0x04,
            textOffset = 0x84,
            GlowColor = 0x8C,
            fxBirthTime = 0x90,
            fadeStartTime = 0x3C,
            fxLetterTime = 0x94,
            fadeTime = 0x40,
            fromColor = 0x38,
            fxDecayStartTime = 0x98,
            fxDecayDuration = 0x9C,
            fontOffset = 0x28,
            fontSizeOffset = 0x14,
            colorOffset = 0x34,
            scaleStartTime = 0x58,
            fromFontScale = 0x18,
            fontScaleTime = 0x20,
            relativeOffset = 0x2c,
            widthOffset = 0x48,
            heightOffset = 0x44,
            shaderOffset = 0x4C,
            alignOffset = 0x30,
            fromAlignOrg = 0x68,
            fromAlignScreen = 0x6C,
            alignOrg = 0x2C,
            alignScreen = 0x30,
            fromY = 0x60,
            fromX = 0x64,
            moveStartTime = 0x70,
            moveTime = 0x74,
            flags = 0xA4,
            soundID = 160U,
            clientIndex = 0xA8;
        }

        public static class HudAlloc
        {
            public static uint
                IndexSlot = 50,
                g_hudelem = 0x012E9858;


            public static bool
                Start = true;
        }


        public static class HUDAlign
        {
            public static uint
                RIGHT = 2,
                CENTER = 5,
                LEFT = 1;
        }


        public class HudTypes
        {
            public static uint
                Text = 1,
                Shader = 6,
                Null = 0;
        }
        public static void ChangeFont(uint elem, uint font)
        {
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.fontOffset, font);
        }
        public class Material
        {
            public static uint
                White = 1,
                Black = 2,
                Prestige0 = 0x1A,
                Prestige1 = 0x1B,
                Prestige2 = 0x1C,
                Prestige3 = 0x1D,
                Prestige4 = 0x1E,
                Prestige5 = 0x1F,
                Prestige6 = 0x20,
                Prestige7 = 0x21,
                Prestige8 = 0x22,
                Prestige9 = 0x23,
                Prestige10 = 0x24,
                WhiteRectangle = 0x25,
                NoMap = 0x29;
        }
        public static int RGB2INT(int r, int g, int b, int a)
        {
            byte[] newRGB = new byte[4];
            newRGB[0] = (byte)r;
            newRGB[1] = (byte)g;
            newRGB[2] = (byte)b;
            newRGB[3] = (byte)a;
            Array.Reverse(newRGB);
            return BitConverter.ToInt32(newRGB, 0);
        }

        public static void SetText(uint clientIndex, uint elem, uint text, uint font, float fontScale, float x, float y, uint alignText, uint align, int r = 255, int g = 255, int b = 255, int a = 255, int GlowR = 255, int GlowG = 0, int GlowB = 0, int GlowA = 0)
        {
            Form1.PS3.Extension.WriteInt32(elem, 0);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.flags, 1);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.clientIndex, clientIndex);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.textOffset, text);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset, alignText);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset - 4, 6);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.fontOffset, font);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.alignOffset, align);
            Form1.PS3.Extension.WriteInt16(elem + HudStruct.textOffset + 4, 0x4000);
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.fontSizeOffset, fontScale);
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.GlowColor, RGB2INT(GlowR, GlowG, GlowB, GlowA));
        }

        public static void SetShader(uint clientIndex, uint elem, string shader, int width, int height, float x, float y, uint align, float sort = 0, int r = 255, int g = 255, int b = 255, int a = 255)
        {
            Form1.PS3.Extension.WriteInt32(elem, 0);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.flags, 1);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.clientIndex, clientIndex);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.shaderOffset, G_MaterialIndex(shader));
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset, 5);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset - 4, HUDAlign.RIGHT);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.heightOffset, height);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.alignOrg, 0);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.alignScreen, 0);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.widthOffset, width);
            Form1.PS3.Extension.WriteUInt32(elem + HudStruct.alignOffset, align);
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.textOffset + 4, sort);
        }

        public static Int32 getLevelTime()
        {
            Byte[] LevelTime = new Byte[4];
            Form1.PS3.GetMemory(0x12e0304, LevelTime);
            Array.Reverse(LevelTime, 0, 4);
            return BitConverter.ToInt32(LevelTime, 0);
        }
        public static void FadeOverTime(uint elem, int Time, int R, int G, int B, int A)
        {
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fadeStartTime, getLevelTime());
            Form1.PS3.Extension.WriteBytes(elem + HudStruct.fromColor, Form1.PS3.Extension.ReadBytes(elem + HudStruct.colorOffset, 4));
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fadeTime, Time);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(R, G, B, A));
        }
        public static void FadeGlowOverTime(uint elem, int Time, int GlowR, int GlowG, int GlowB, int GlowA)
        {
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fadeStartTime, getLevelTime());
            Form1.PS3.Extension.WriteBytes(elem + HudStruct.fromColor, Form1.PS3.Extension.ReadBytes(elem + HudStruct.colorOffset, 4));
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fadeTime, Time);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.GlowColor, RGB2INT(GlowR, GlowG, GlowB, GlowA));
        }

        public static void MoveOverTime(uint elem, Int32 time, float x, float y)
        {
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fromAlignOrg, Form1.PS3.Extension.ReadInt32(elem + HudStruct.alignOrg));
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fromAlignScreen, Form1.PS3.Extension.ReadInt32(elem + HudStruct.alignScreen));
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.fromY, Form1.PS3.Extension.ReadFloat(elem + HudStruct.yOffset));
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.fromX, Form1.PS3.Extension.ReadFloat(elem + HudStruct.xOffset));
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.moveStartTime, getLevelTime());
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.moveTime, time);
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
            Form1.PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
        }
        public static void setPulseFX(uint elem, int speed, int decayStart, int decayDuration)
        {
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fxBirthTime, getLevelTime());
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fxLetterTime, speed);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fxDecayStartTime, decayStart);
            Form1.PS3.Extension.WriteInt32(elem + HudStruct.fxDecayDuration, decayDuration);
        }
        public static void DestroyElem(uint elem)
        {
            Form1.PS3.SetMemory(elem, new byte[0xB4]);
        }

        public static void SetElement(uint Element, uint HudTypes)
        {
            Form1.PS3.Extension.WriteUInt32(Element, HudTypes);
        }
        public static uint HudElemAlloc(bool Reset = false)
        {
            if (Reset == true)
                HudAlloc.IndexSlot = 50;
            uint Output = HudAlloc.g_hudelem + (HudAlloc.IndexSlot * 0xB4);
            HudAlloc.IndexSlot++;
            return Output;
        }
        public static uint HudElemAlloc_Game(int clientNumber)
        {
            return (uint)Call(0x001806E0, clientNumber);
        }
        public static void WritePowerPc(bool Active)
        {
            byte[] NewPPC = new byte[] { 0xF8, 0x21, 0xFF, 0x61, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x01, 0x00, 0xB0, 0x3C, 0x60, 0x10, 0x03, 0x80, 0x63, 0x00, 0x00, 0x60, 0x62, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x04, 0x80, 0x63, 0x00, 0x00, 0x2C, 0x03, 0x00, 0x00, 0x41, 0x82, 0x00, 0x28, 0x3C, 0x60, 0x10, 0x04, 0x80, 0x63, 0x00, 0x04, 0x3C, 0xA0, 0x10, 0x04, 0x38, 0x80, 0x00, 0x00, 0x30, 0xA5, 0x00, 0x10, 0x4B, 0xE8, 0xB2, 0x7D, 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x10, 0x04, 0x90, 0x64, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x05, 0x80, 0x63, 0x00, 0x00, 0x2C, 0x03, 0x00, 0x00, 0x41, 0x82, 0x00, 0x24, 0x3C, 0x60, 0x10, 0x05, 0x30, 0x63, 0x00, 0x10, 0x4B, 0xE2, 0xF9, 0x7D, 0x3C, 0x80, 0x10, 0x05, 0x90, 0x64, 0x00, 0x04, 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x10, 0x05, 0x90, 0x64, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x03, 0x80, 0x63, 0x00, 0x04, 0x60, 0x62, 0x00, 0x00, 0xE8, 0x01, 0x00, 0xB0, 0x7C, 0x08, 0x03, 0xA6, 0x38, 0x21, 0x00, 0xA0, 0x4E, 0x80, 0x00, 0x20 };
            byte[] RestorePPC = new byte[] { 0x81, 0x62, 0x92, 0x84, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x21, 0xFF, 0x01, 0xFB, 0xE1, 0x00, 0xB8, 0xDB, 0x01, 0x00, 0xC0, 0x7C, 0x7F, 0x1B, 0x78, 0xDB, 0x21, 0x00, 0xC8, 0xDB, 0x41, 0x00, 0xD0, 0xDB, 0x61, 0x00, 0xD8, 0xDB, 0x81, 0x00, 0xE0, 0xDB, 0xA1, 0x00, 0xE8, 0xDB, 0xC1, 0x00, 0xF0, 0xDB, 0xE1, 0x00, 0xF8, 0xFB, 0x61, 0x00, 0x98, 0xFB, 0x81, 0x00, 0xA0, 0xFB, 0xA1, 0x00, 0xA8, 0xFB, 0xC1, 0x00, 0xB0, 0xF8, 0x01, 0x01, 0x10, 0x81, 0x2B, 0x00, 0x00, 0x88, 0x09, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00, 0x40, 0x9E, 0x00, 0x64, 0x7C, 0x69, 0x1B, 0x78, 0xC0, 0x02, 0x92, 0x94, 0xC1, 0xA2, 0x92, 0x88, 0xD4, 0x09, 0x02, 0x40, 0xD0, 0x09, 0x00, 0x0C, 0xD1, 0xA9, 0x00, 0x04, 0xD0, 0x09, 0x00, 0x08, 0xE8, 0x01, 0x01, 0x10, 0xEB, 0x61, 0x00, 0x98, 0xEB, 0x81, 0x00, 0xA0, 0x7C, 0x08, 0x03, 0xA6, 0xEB, 0xA1, 0x00, 0xA8, 0xEB, 0xC1, 0x00, 0xB0, 0xEB, 0xE1, 0x00, 0xB8, 0xCB, 0x01, 0x00, 0xC0, 0xCB, 0x21, 0x00, 0xC8 };
            if (Active)
                Form1.PS3.SetMemory(0x0038EDE8, NewPPC);
            else
                Form1.PS3.SetMemory(0x0038EDE8, RestorePPC);
        }
        public static uint G_LocalizedString(string input)
        {
            uint StrIndex = 0;
            bool isRunning = true;
            WritePowerPc(true);
            Form1.PS3.Extension.WriteString(0x10050010, input);
            Form1.PS3.Extension.WriteBool(0x10050000 + 3, true);
            do { StrIndex = Form1.PS3.Extension.ReadUInt32(0x10050004); } while (StrIndex == 0);
            Form1.PS3.Extension.WriteUInt32(0x10050004, 0);
            do { isRunning = Form1.PS3.Extension.ReadBool(0x10050003); } while (isRunning != false);
            WritePowerPc(false);
            return StrIndex;
        }
        public static uint G_MaterialIndex(string shader)
        {
            return (uint)Call(0x001BE758, shader);
        }

        private static byte[] ReverseBytes(byte[] inArray)
        {
            Array.Reverse(inArray);
            return inArray;
        }
        public static void WriteSingle(uint address, float[] input)
        {
            int length = input.Length;
            byte[] array = new byte[length * 4];
            for (int i = 0; i < length; i++)
            {
                ReverseBytes(BitConverter.GetBytes(input[i])).CopyTo(array, (int)(i * 4));
            }
            Form1.PS3.SetMemory(address, array);
        }
        public static void ChangeText(uint Element, string newText)
        {
            Form1.PS3.Extension.WriteUInt32(Element + HudStruct.textOffset, G_LocalizedString(newText));
        }
        public static UInt32 client_s(Int32 clientIndex)
        {
            return 0x34740000 + (0x97F80 * (UInt32)clientIndex);
        }

        public static UInt32 G_Entity(Int32 clientIndex)
        {
            return 0x1319800 + (0x280 * (UInt32)clientIndex);
        }
        public static UInt32 G_Client(Int32 clientIndex)
        {
            return 0x14E2200 + (0x3700 * (UInt32)clientIndex);
        }
        public class Buttons
        {
            public static string
                DpadUp = "+actionslot 1",
                DpadDown = "+actionslot 2",
                DpadRight = "+actionslot 4",
                DpadLeft = "+actionslot 3",
                Cross = "+gostand",
                Circle = "+stance",
                Triangle = "weapnext",
                Square = "+usereload",
                R3 = "+melee",
                R2 = "+frag",
                R1 = "+attack",
                L3 = "+breath_sprint",
                L2 = "+smoke",
                L1 = "+speed_throw",
                Select = "togglescores",
                Start = "togglemenu";
        }
        public static bool ButtonPressed(int client, string Button)
        {
            if (Form1.PS3.Extension.ReadString(0x34750E9F + ((uint)client * 0x97F80)) == Button)
                return true;
            else return false;
        }
        public static String KeyBoard(String Title, String PresetText = "", Int32 MaxLength = 20)
        {
            Call(0x238070, 0, Title, PresetText, MaxLength, 0x70B4D8);
            while (Form1.PS3.Extension.ReadInt32(0x203B4C8) != 0) continue;
            return Form1.PS3.Extension.ReadString(0x2510E22);
        }

        public static Boolean IsMW2()
        {
            foreach (String temp in new String[] { "BLUS30377", "BLKS20159", "BLES00683", "BLES00686", "BLES00685", "BLES00684", "BLES00687" })
                if (temp == Form1.PS3.Extension.ReadString(0x10010251))
                    return true;
            return false;
        }
        public static void cBuff_AddTextFIX(string dvar)
        {
            Form1.PS3.SetMemory(0x2005000, Encoding.UTF8.GetBytes("\nset CbufFIX \"set CbufFIX vstr CbufFIX0;" + dvar + "\""));
            byte[] RPCON = new byte[] { 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x02, 0x00, 0x30, 0x84, 0x50, 0x00, 0x4B, 0xF8, 0x63, 0xFD };
            Form1.PS3.SetMemory(0x253AB8, RPCON);
            Thread.Sleep(10);
            byte[] DFT1 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            Form1.PS3.SetMemory(0x2005000, DFT1);
            Form1.PS3.SetMemory(0x2005000, Encoding.UTF8.GetBytes("\nvstr CbufFIX\nset CbufFIX \" \""));
            Thread.Sleep(10);
            byte[] RPCOFF = new byte[] { 0x81, 0x22, 0x45, 0x10, 0x81, 0x69, 0x00, 0x00, 0x88, 0x0B, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00 };
            Form1.PS3.SetMemory(0x253AB8, RPCOFF);
            byte[] DFT2 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            Form1.PS3.SetMemory(0x2005000, DFT2);
        }

        public static void cBuff_AddTextSay(string dvar)
        {
            Form1.PS3.SetMemory(0x2005000, Encoding.UTF8.GetBytes(";" + "say " + dvar));
            byte[] RPCON = new byte[] { 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x02, 0x00, 0x30, 0x84, 0x50, 0x00, 0x4B, 0xF8, 0x63, 0xFD };
            Form1.PS3.SetMemory(0x253AB8, RPCON);
            Thread.Sleep(15);
            byte[] RPCOFF = new byte[] { 0x81, 0x22, 0x45, 0x10, 0x81, 0x69, 0x00, 0x00, 0x88, 0x0B, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00 };
            Form1.PS3.SetMemory(0x253AB8, RPCOFF);
            byte[] DFT2 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            Form1.PS3.SetMemory(0x2005000, DFT2);
        }
        public static void cBuff_AddTextReg(string dvar)
        {
            Form1.PS3.SetMemory(0x2005000, Encoding.UTF8.GetBytes(";" + dvar + ";"));
            byte[] RPCON = new byte[] { 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x02, 0x00, 0x30, 0x84, 0x50, 0x00, 0x4B, 0xF8, 0x63, 0xFD };
            Form1.PS3.SetMemory(0x253AB8, RPCON);
            Thread.Sleep(15);
            byte[] RPCOFF = new byte[] { 0x81, 0x22, 0x45, 0x10, 0x81, 0x69, 0x00, 0x00, 0x88, 0x0B, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00 };
            Form1.PS3.SetMemory(0x253AB8, RPCOFF);
            byte[] DFT2 = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            Form1.PS3.SetMemory(0x2005000, DFT2);
        }
        public static void cBuff_AddText(int clientNum, string command)
        {
            Call(0x001D9EC0, clientNum, "\n" + "\n" + command);
        }
        public static void SV_GameSendServerCommand(Int32 clientIndex, String Cmd)
        {
            Call(0x0021A0A0, clientIndex, 0, Cmd);
        }
        public static void setClientDvar(int clientNumber, string dvar, string Val)
        {
            SV_GameSendServerCommand(clientNumber, "v " + dvar + " \"" + Val + "\"");
        }
        public static void setClientJustDvar(int clientNumber, string dvar)
        {
            SV_GameSendServerCommand(clientNumber, "v " + dvar);
        }
        public static void iPrintln(int clientNumber, string Txt)
        {
            SV_GameSendServerCommand(clientNumber, "f \"" + Txt + "\"");
        }
        public static void iPrintlnBold(int clientNumber, string Txt)
        {
            SV_GameSendServerCommand(clientNumber, "g \"" + Txt + "\"");
        }

        public static void Notivation(int clientNumber, string Txt)
        {
            SV_GameSendServerCommand(clientNumber, "g \"" + "                                                                      " + Txt + "\"");
        }

        public static void playSound(int clientNumber, string soundName)
        {
            SV_GameSendServerCommand(clientNumber, "o \"" + soundName + "\"");
        }


        public static void SV_SendServerCommand(int clientIndex, string Command)
        {
            WritePowerPc(true);
            Form1.PS3.Extension.WriteString(0x10040010, Command);
            Form1.PS3.Extension.WriteInt32(0x10040004, clientIndex);
            Form1.PS3.Extension.WriteBool(0x10040003, true);
            bool isRunning;
            do { isRunning = Form1.PS3.Extension.ReadBool(0x10040003); } while (isRunning != false);
            WritePowerPc(false);
        }


        public static void SetShader(uint index, uint client, uint shader, int width, int height, float x, float y, uint align, float sort = 0, int r = 255, int g = 255, int b = 255, int a = 255)
        {
            uint Ind = Convert.ToUInt32(index);
            uint Elem = 0x12E9858 + ((uint)Ind * 0xB4);
            Form1.PS3.Extension.WriteInt32(Elem, 6);
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.flags, 1);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.clientIndex, client);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.shaderOffset, shader);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.relativeOffset, 5);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.relativeOffset - 4, 6);
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.heightOffset, height);
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.widthOffset, width);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.alignOffset, align);
            Form1.PS3.Extension.WriteFloat(Elem + HudStruct.xOffset, x);
            Form1.PS3.Extension.WriteFloat(Elem + HudStruct.yOffset, y);
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
        }
        public static void doTypeWriter(uint index, uint client, string text, uint font, float fontScale, float x, float y, uint alignText, uint align, int r = 255, int g = 255, int b = 255, int a = 255, int GlowR = 255, int GlowG = 0, int GlowB = 0, int GlowA = 0)
        {
            uint Ind = Convert.ToUInt32(index);
            uint Elem = 0x12E9858 + ((uint)Ind * 0xB4);
            Form1.PS3.Extension.WriteInt32(Elem, 1);
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.flags, 1);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.clientIndex, client);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.textOffset, G_LocalizedString(text));
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.relativeOffset, alignText);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.relativeOffset - 4, 6);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.fontOffset, font);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.alignOffset, align);
            Form1.PS3.Extension.WriteInt16(Elem + HudStruct.textOffset + 4, 0x4000);
            Form1.PS3.Extension.WriteFloat(Elem + HudStruct.fontSizeOffset, fontScale);
            Form1.PS3.Extension.WriteFloat(Elem + HudStruct.xOffset, x);
            Form1.PS3.Extension.WriteFloat(Elem + HudStruct.yOffset, y);
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.GlowColor, RGB2INT(GlowR, GlowG, GlowB, GlowA));
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.fxBirthTime, getLevelTime());
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.fxLetterTime, 100);
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.fxDecayStartTime, 4000);
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.fxDecayDuration, 1000);
            Form1.PS3.Extension.WriteInt32(Elem + HudStruct.flags, 1);
            Form1.PS3.Extension.WriteUInt32(Elem + HudStruct.textOffset, G_LocalizedString(text));
        }

        public static UInt32 G_Client(int clientIndex, UInt32 Mod)
        {
            return (MW2Lib.Offsets.G_Client + (UInt32)Mod) + ((UInt32)clientIndex * HudStruct.clientIndex);
        }
        #region server details
        #region Boolean's
        public static Boolean InDaGame()
        {
            return Form1.PS3.Extension.ReadBool(0x01D17A8C);
        }
        private static String ReturnInfos(Int32 Index)
        {
            byte[] buffer = new byte[0x234];
            Form1.PS3.GetMemory(0x17A54E0, buffer);
            return Encoding.ASCII.GetString(buffer).Replace(@"\", "|").Split('|')[Index];
        }
        private static String OnlineInfos(Int32 Index)
        {
            byte[] buffer = new byte[0x234];
            Form1.PS3.GetMemory(0x009aa2d9, buffer);
            return Encoding.ASCII.GetString(buffer).Replace(@"\", "|").Split('|')[Index];
        }
        private static Boolean Online()
        {
            if (ReturnInfos(1) == "cg_predictItems")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        #region Host Name?
        public static String getHostName()
        {
            if (InDaGame())
            {
                if (Online())
                {
                    return ReturnInfos(8);
                }
                else
                {
                    return ReturnInfos(16);
                }
            }
            else { return "Not in game"; }
        }
        #endregion
        #region Max Players?
        public static String getMaxPlayers()
        {
            if (InDaGame())
            {
                if (Online())
                {
                    return OnlineInfos(18);
                }
                else
                {
                    return ReturnInfos(18);
                }
            }
            else { return "Not in game"; }
        }
        #endregion
        #region Gamemode
        public static String getGameMode()
        {
            if (InDaGame())
            {
                if (Online())
                {
                    //Online
                    switch (OnlineInfos(2))
                    {
                        default: return "Unknown Gametype";
                        case "1": return "Loading Game";
                        case "war": return "Team Deathmatch";
                        case "dm": return "Free for All";
                        case "sd": return "Search and Destroy";
                        case "dom": return "Domination";
                        case "dem": return "Demolition";
                        case "gtnw": return "Global Thermonuclear War";
                        case "ctf": return "Capture The Flag";
                        case "arena": return "Arena";
                    }
                }
                else
                {
                    //Private Match
                    switch (ReturnInfos(2))
                    {
                        default: return "Unknown Gametype";
                        case "1": return "Loading Game";
                        case "war": return "Team Deathmatch";
                        case "dm": return "Free for All";
                        case "sd": return "Search and Destroy";
                        case "dom": return "Domination";
                        case "dem": return "Demolition";
                        case "gtnw": return "Global Thermonuclear War";
                        case "ctf": return "Capture The Flag";
                        case "arena": return "Arena";
                    }
                }
            }
            else { return "Not in game"; }
        }
        #endregion
        #region Hardcore?
        public static String getHardcore()
        {
            if (InDaGame())
            {
                if (Online())
                {
                    switch (OnlineInfos(4))
                    {
                        default: return "Unknown Gametype";
                        case "20000": return "Loading Game";
                        case "0": return "Off";
                        case "1": return "On";
                    }
                }
                else
                {
                    switch (ReturnInfos(4))
                    {
                        default: return "Unknown Gametype";
                        case "20000": return "Loading Game";
                        case "0": return "Off";
                        case "1": return "On";
                    }
                }
            }
            else { return "Not in game"; }
        }
        #endregion
        #region Map
        public static String get_MapName()
        {
            String str = Form1.PS3.Extension.ReadString(0xD495F4), MapStr = "Not in game";
            if (InDaGame())
            {
                if (str.Contains("afghan"))
                    MapStr = "Afghan";
                if (str.Contains("highrise"))
                    MapStr = "Highrise";
                if (str.Contains("rundown"))
                    MapStr = "Rundown";
                if (str.Contains("quarry"))
                    MapStr = "Quarry";
                if (str.Contains("nightshift"))
                    MapStr = "Skidrow";
                if (str.Contains("terminal"))
                    MapStr = "Terminal";
                if (str.Contains("brecourt"))
                    MapStr = "Wasteland";
                if (str.Contains("derail"))
                    MapStr = "Derail";
                if (str.Contains("estate"))
                    MapStr = "Estate";
                if (str.Contains("favela"))
                    MapStr = "Favela";
                if (str.Contains("invasion"))
                    MapStr = "Invasion";
                if (str.Contains("rust"))
                    MapStr = "Rust";
                if (str.Contains("scrapyard") || str.Contains(("boneyard")))
                    MapStr = "Scrapyard";
                if (str.Contains("sub"))
                    MapStr = "Subbase";
                if (str.Contains("underpass"))
                    MapStr = "Underpass";
                if (str.Contains("checkpoint"))
                    MapStr = "Karachi";
                if (str.Contains("bailout"))
                    MapStr = "Bailout";
                if (str.Contains("compact"))
                    MapStr = "Salvage";
                if (str.Contains("storm") || str.Contains(("storm2")))
                    MapStr = "Storm";
                if (str.Contains("crash"))
                    MapStr = "Crash";
                if (str.Contains("overgrown"))
                    MapStr = "Overgrown";
                if (str.Contains("strike"))
                    MapStr = "Strike";
                if (str.Contains("vacant"))
                    MapStr = "Vacant";
                if (str.Contains("trailerpark"))
                    MapStr = "Trailer Park";
                if (str.Contains("fuel"))
                    MapStr = "Fuel";
                if (str.Contains("abandon"))
                    MapStr = "Carnival";
                if (str.Contains("dlc2_ui_mp"))
                    MapStr = "Not in game";
            }
            return MapStr;
        }
        #endregion
        #endregion
    }
}