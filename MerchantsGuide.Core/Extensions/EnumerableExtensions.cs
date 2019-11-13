﻿using System;
using System.Collections.Generic;

namespace JustinWare.MerchantsGuide.Core.Extensions
{
  public static class EnumerableExtensions
  {
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (var item in source)
      {
        action(item);
      }
    }
  }
}
