using System;
using System.Collections.ObjectModel;

/// <summary>
/// 配列の拡張メソッドを管理するクラス
/// </summary>
public static class ArrayExtensions
{
	/// <summary>
	/// ランダムに並び替えた新しい配列を返します
	/// </summary>
	public static T[] Shuffle<T>(this T[] array)
	{
		var length = array.Length;
		var result = new T[length];
		Array.Copy(array, result, length);
		
		var random = new Random();
		int n = length;
		while (1 < n)
		{
			n--;
			int k = random.Next(n + 1);
			var tmp = result[k];
			result[k] = result[n];
			result[n] = tmp;
		}
		return result;
	}
}