// ***********************************************************
// Type: Gomoku.SettingsBase
// Assembly: Gomoku, Version=9.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D2AD535-72EC-4350-94A2-579EE27B5143
// ***********************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Windows.Storage;

#nullable disable
namespace Gomoku
{
  internal partial class SettingsBase
  {
    private ApplicationDataContainer myIsolatedStorageSettings = ApplicationData.Current.LocalSettings;
    private Dictionary<string, object> myChachedStorage = new Dictionary<string, object>();

    protected void AddOrUpdateValue(string key, object value)
    {
      try
      {
        if (this.myIsolatedStorageSettings == null)
          this.myIsolatedStorageSettings = ApplicationData.Current.LocalSettings;
        int? nullable = new int?();
        try
        {
          nullable = new int?((int) value);
        }
        catch
        {
        }
        if (!nullable.HasValue)
        {
          if (value is int[][])
            this.WriteToFile("table.txt", SettingsBase.Serialize(value));
          else
            ((IDictionary<string, object>) this.myIsolatedStorageSettings.Values)[key] = value;
        }
        else
          ((IDictionary<string, object>) this.myIsolatedStorageSettings.Values)[key] = (object) nullable;
        this.UpdateCache(key, value);
      }
      catch
      {
      }
    }

    protected T TryGetValue<T>(string key)
    {
      try
      {
        object obj1;
        if (this.myChachedStorage.TryGetValue(key, out obj1))
          return (T) obj1;
        T obj2 = default (T);
        if ((object) typeof (T) == (object) typeof (int[][]))
          obj2 = SettingsBase.Deserialize<T>(this.ReadFromFile("table.txt"));
        if (this.myIsolatedStorageSettings == null)
          this.myIsolatedStorageSettings = ApplicationData.Current.LocalSettings;
        if (((IDictionary<string, object>) this.myIsolatedStorageSettings.Values).ContainsKey(key))
          obj2 = (T) ((IDictionary<string, object>) this.myIsolatedStorageSettings.Values)[key];
        this.UpdateCache(key, (object) obj2);
        return obj2;
      }
      catch
      {
        return default (T);
      }
    }

    private void UpdateCache(string key, object value)
    {
      if (this.myChachedStorage.ContainsKey(key))
        this.myChachedStorage[key] = value;
      else
        this.myChachedStorage.Add(key, value);
    }

    private static string Serialize(object obj)
    {
      try
      {
        using (StringWriter stringWriter = new StringWriter())
        {
          new XmlSerializer(obj.GetType()).Serialize((TextWriter) stringWriter, obj);
          return stringWriter.ToString();
        }
      }
      catch
      {
        return (string) null;
      }
    }

    private static T Deserialize<T>(string xml)
    {
      try
      {
        using (StringReader stringReader = new StringReader(xml))
          return (T) new XmlSerializer(typeof (T)).Deserialize((TextReader) stringReader);
      }
      catch
      {
        return default (T);
      }
    }

    private async void WriteToFile(string fileName, string data)
    {
      try
      {
        await FileIO.WriteTextAsync((IStorageFile) await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, (CreationCollisionOption) 1), data);
      }
      catch (Exception ex)
      {
         Debug.WriteLine("[ex] Settings - WriteToFile error: " + ex.Message);
      }
    }

    private string ReadFromFile(string fileName)
    {
      try
      {
        return FileIO.ReadTextAsync((IStorageFile) ApplicationData.Current.LocalFolder.GetFileAsync(fileName)
            .AsTask<StorageFile>().GetAwaiter().GetResult()).AsTask<string>().GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[info] Settings - ReadToFile problem: " + ex.Message);
        return null;
      }
    }
  }
}
