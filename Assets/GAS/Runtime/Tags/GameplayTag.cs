using System;
using System.Linq;
using UnityEngine;

namespace GAS.Runtime
{
    [Serializable]
    public struct GameplayTag
    {
        [SerializeField] private string _name;
        [SerializeField] private int _hashCode;
        [SerializeField] private string _shortName;
        [SerializeField] private int[] _ancestorHashCodes;
        [SerializeField] private string[] _ancestorNames;

        public GameplayTag(string name)
        {
            _name = name;
            _hashCode = name.GetHashCode();

            var tags = name.Split('.');
            // if (tags.Length > GasDefine.GAS_TAG_MAX_GENERATIONS)
            //     throw new Exception(
            //         $"GameplayTag {name} has more than {GasDefine.GAS_TAG_MAX_GENERATIONS} generations");

            _ancestorNames = new string[tags.Length - 1];
            _ancestorHashCodes = new int[tags.Length - 1];
            var ancestorTag = "";
            for (var i = 0; i < tags.Length - 1; i++)
            {
                ancestorTag += tags[i];
                _ancestorHashCodes[i] = ancestorTag.GetHashCode();
                _ancestorNames[i] = ancestorTag;
                ancestorTag += ".";
            }

            _shortName = tags.Last();
        }

        /// <summary>
        ///     Only For Show.
        /// </summary>
        public string Name => _name;

        /// <summary>
        ///     Only For Show.
        /// </summary>
        public string ShortName => _shortName;

        /// <summary>
        ///     Actually ,Use the hash code for compare.
        /// </summary>
        public int HashCode => _hashCode;

        public string[] AncestorNames => _ancestorNames;

        public bool Root => _ancestorHashCodes.Length == 0;

        public int[] AncestorHashCodes => _ancestorHashCodes;

        public bool IsDescendantOf(GameplayTag other)
        {
            // 感觉这里对接口的解释是反的？
            // return other._ancestorHashCodes.Contains(HashCode);
            return _ancestorHashCodes.Contains(other.HashCode);
        }

        public override bool Equals(object obj)
        {
            return obj is GameplayTag tag && this == tag;
        }

        public override int GetHashCode()
        {
            return HashCode;
        }

        public static bool operator ==(GameplayTag x, GameplayTag y)
        {
            return x.HashCode == y.HashCode;
        }

        public static bool operator !=(GameplayTag x, GameplayTag y)
        {
            return x.HashCode != y.HashCode;
        }

        public bool HasTag(GameplayTag tag)
        {
            foreach (var ancestorHashCode in _ancestorHashCodes)
                if (ancestorHashCode == tag.HashCode)
                    return true;

            return this == tag;
        }
    }
}