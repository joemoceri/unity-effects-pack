namespace APPack
{
    using System;
    using UnityEngine;

    public class EnumPropertyDrawerAttribute : PropertyAttribute
    {
        public Type enumType;

        public EnumPropertyDrawerAttribute(Type enumType)
        {
            this.enumType = enumType;
        }
    }
}