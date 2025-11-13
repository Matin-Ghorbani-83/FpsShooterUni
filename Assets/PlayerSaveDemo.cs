using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

[Serializable]
public class Vector3Serializable
{
    public float x, y, z;

    public Vector3Serializable() { }
    public Vector3Serializable(Vector3 v) { x = v.x; y = v.y; z = v.z; }
    public Vector3 ToVector3() => new Vector3(x, y, z);
}

[Serializable]
public class PlayerSaveData
{
    public Vector3Serializable position;

    public PlayerSaveData() { }
    public PlayerSaveData(Vector3 pos)
    {
        position = new Vector3Serializable(pos);
    }
}

public class PlayerSaveDemo : MonoBehaviour
{
    [Header("Encryption (AES)")]
    public bool useEncryption = false;

    static readonly byte[] AesKey = new byte[32] {
        21,12,33,44,55,66,77,88,99,10,11,12,13,14,15,16,
        17,18,19,20,31,32,43,54,65,76,87,98,109,110,121,132
    };
    static readonly byte[] AesIV = new byte[16] {
        1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
    };

    [Header("File Names")]
    public string jsonFileName = "player.json";
    public string encryptedFileName = "player.dat";

    string RootPath => Application.persistentDataPath;

    byte[] AESEncrypt(byte[] plain)
    {
        using var aes = Aes.Create();
        aes.Key = AesKey;
        aes.IV = AesIV;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(plain, 0, plain.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }
    }

    byte[] AESDecrypt(byte[] cipher)
    {
        using var aes = Aes.Create();
        aes.Key = AesKey;
        aes.IV = AesIV;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(cipher, 0, cipher.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }
    }

    public void Save()
    {
        try
        {
            var data = new PlayerSaveData(transform.position);

            if (!useEncryption)
            {
                string json = JsonUtility.ToJson(data, true);
                string path = Path.Combine(RootPath, jsonFileName);
                File.WriteAllText(path, json, Encoding.UTF8);
                Debug.Log($"[SAVE] JSON -> {path}\n{json}");
            }
            else
            {
                string json = JsonUtility.ToJson(data, false);
                byte[] plain = Encoding.UTF8.GetBytes(json);
                byte[] cipher = AESEncrypt(plain);
                string path = Path.Combine(RootPath, encryptedFileName);
                File.WriteAllBytes(path, cipher);
                Debug.Log($"[SAVE] ENCRYPTED -> {path} (len={cipher.Length})");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[SAVE] Failed: " + e);
        }
    }

    public void Load()
    {
        try
        {
            string encPath = Path.Combine(RootPath, encryptedFileName);
            string jsonPath = Path.Combine(RootPath, jsonFileName);
            PlayerSaveData data = null;

            if (useEncryption && File.Exists(encPath))
            {
                byte[] cipher = File.ReadAllBytes(encPath);
                byte[] plain = AESDecrypt(cipher);
                string text = Encoding.UTF8.GetString(plain);
                data = JsonUtility.FromJson<PlayerSaveData>(text);
                Debug.Log($"[LOAD] ENCRYPTED <- {encPath}\n{text}");
            }
            else if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath, Encoding.UTF8);
                data = JsonUtility.FromJson<PlayerSaveData>(json);
                Debug.Log($"[LOAD] JSON <- {jsonPath}\n{json}");
            }
            else
            {
                Debug.LogWarning("[LOAD] No save file found.");
                return;
            }

            ApplyData(data);
        }
        catch (Exception e)
        {
            Debug.LogError("[LOAD] Failed: " + e);
        }
    }

    void ApplyData(PlayerSaveData data)
    {
        if (data == null || data.position == null)
        {
            Debug.LogWarning("[APPLY] Data is null/incomplete.");
            return;
        }

        transform.position = data.position.ToVector3();
        Debug.Log($"[APPLY] position -> {transform.position}");
    }


}
