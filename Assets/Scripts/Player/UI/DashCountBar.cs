using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Util;

namespace Player.UI
{
  public class DashCountBar : MonoBehaviour
  {
    private IObjectPool<Image> pool;
    private List<Image> activedItems = new();

    public string itemPrefab;

    private DashType tmpType;

    private readonly Dictionary<DashType, Color> dashTypeSet = new()
    {
      { DashType.Normal, Color.magenta },
      { DashType.Additional, Color.yellow }
    };

    private void Awake()
    {
      pool = new LinkedPool<Image>
      (
        () => Instantiate(GameManager.Prefabs[itemPrefab], transform).GetComponent<Image>(),
        obj =>
        {
          obj.name = tmpType.ToString();
          obj.color = dashTypeSet[tmpType];
          activedItems.Add(obj);
          obj.gameObject.SetActive(true);
        },
        obj =>
        {
          activedItems.Remove(obj);
          obj.gameObject.SetActive(false);
        },
        obj => Destroy(obj.gameObject)
      );
    }

    public void SetCount(int value = 0, int additional = 0)
    {
      ClearCount();
      AddCount(value, DashType.Normal);
      AddCount(additional, DashType.Additional);
    }

    public void AddCount(int value = 1, DashType type = DashType.Normal)
    {
      value.For(() =>
      {
        tmpType = type;
        pool.Get();
      });
      
      RefreshCapacity();
    }

    public void SubCount(int value = 1)
    {
      value.For(() =>
      {
        if (activedItems.Exists(image => image.name == DashType.Additional.ToString()))
        {
          var obj = activedItems.LastOrDefault(x => x.name == DashType.Additional.ToString());
          if (obj is not null) pool.Release(obj);
        }
        else
        {
          var obj = activedItems.LastOrDefault(x => x.name == DashType.Normal.ToString());
          if (obj is not null) pool.Release(obj);
        }
      });
      
      RefreshCapacity();
    }

    public void ClearCount()
    {
      Debug.Log(activedItems.Count);
      activedItems.ForEach(obj => pool.Release(obj));
    }

    private void RefreshCapacity()
    {
      var i = 0;
      foreach (var obj in activedItems.FindAll(x => x.name == DashType.Normal.ToString()))
      {
        obj.transform.SetAsFirstSibling();
        i++;
      }

      foreach (var obj in activedItems.FindAll(x => x.name == DashType.Additional.ToString()))
      {
        obj.transform.SetAsLastSibling();
        i++;
      }
    }
  }
}
