using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using WBase.Core.Extensions;
using WBase.Core.Util;

public class ScoreManager : MonoBehaviour
{
    public class HiScore
    {
        public string Initials { get; private set; }
        public int Score { get; private set; }

        public HiScore() { }

        public HiScore(string Initials, int Score)
        {
            this.Initials = Initials.ToUpper();
            this.Score = Score;
        }

        public byte[] ToBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(System.Text.Encoding.ASCII.GetBytes(Initials));
            bytes.AddRange(BitConverter.GetBytes(Score));
            return bytes.ToArray();
        }

        public static HiScore FromBytes(byte[] bytes)
        {
            byte[] workingBytes = bytes;
            HiScore output = new HiScore();
            output.Initials = BitConverter.ToString(workingBytes, 0);
            output.Score = BitConverter.ToInt32(workingBytes, output.Initials.Length * ByteUtil.BytesIn(typeof(char)));
            return output;
        }
    }

    public int CurrentScore = 0;
    private const int MaxHiScores = 25;
    public const string SaveFilePath = "./SaveData/HiScores.dat";
    public static readonly HiScore[] DefaultHiScores = new HiScore[MaxHiScores]
    {
        new HiScore("GOD", 50000),
        new HiScore("MOM", 40000),
        new HiScore("DAD", 30000),
        new HiScore("ABC", 20000),
        new HiScore("ABC", 10000),
        new HiScore("ABC", 9000),
        new HiScore("ABC", 8000),
        new HiScore("ABC", 7000),
        new HiScore("ABC", 6000),
        new HiScore("ABC", 5000),
        new HiScore("ABC", 4500),
        new HiScore("ABC", 4250),
        new HiScore("ABC", 3000),
        new HiScore("ABC", 2750),
        new HiScore("ABC", 2500),
        new HiScore("ABC", 2250),
        new HiScore("ABC", 1000),
        new HiScore("ABC", 900),
        new HiScore("ABC", 800),
        new HiScore("ABC", 700),
        new HiScore("ABC", 600),
        new HiScore("ABC", 500),
        new HiScore("ABC", 400),
        new HiScore("ABC", 300),
        new HiScore("BAD", 200)
    };

    private FileStream HiScoreFile;

    public HiScore[] HiScores = new HiScore[25];

    public ScoreManager()
    {
        HiScoreFile = File.Open(SaveFilePath, FileMode.OpenOrCreate);
        if (HiScoreFile.Length == 0)
        {
            ResetHiScores();
        }
        else
        {
            try
            {
                byte[] buffer = new byte[int.MaxValue];

                HiScoreFile.Read(buffer, 0, (int)HiScoreFile.Length);
                int NumScores = BitConverter.ToInt32(buffer, 0);
                buffer = buffer.SubBytes(ByteUtil.BytesIn(typeof(int)));
                for (int i = 0; i < NumScores; i++)
                {
                    int InitialsLength = BitConverter.ToInt32(buffer, 0);
                    byte[] SingleScoreBuffer = new byte[2 * ByteUtil.BytesIn(typeof(int)) + (InitialsLength * ByteUtil.BytesIn(typeof(char)))];
                    int j = 0;
                    for (; j < SingleScoreBuffer.Length; j++)
                    {
                        SingleScoreBuffer[j] = buffer[j];
                    }
                    buffer = buffer.SubBytes(j);
                    HiScores[i] = HiScore.FromBytes(SingleScoreBuffer);
                }
            }
            catch(Exception ex)
            {
                Debug.LogError("Exception loading HiScores, resetting HiScores");
                Debug.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
                ResetHiScores();
            }
        }
    }

    private void ResetHiScores()
    {
        HiScores = DefaultHiScores;

        int FileOffSet = 0;

        HiScoreFile.Write(BitConverter.GetBytes(MaxHiScores), FileOffSet, ByteUtil.BytesIn(typeof(int)));
        FileOffSet += ByteUtil.BytesIn(typeof(int));

        foreach (HiScore hiScore in HiScores)
        {
            byte[] SaveBytes = hiScore.ToBytes();
            HiScoreFile.Write(SaveBytes, FileOffSet, SaveBytes.Length);
            FileOffSet += SaveBytes.Length;
        }
    }
}