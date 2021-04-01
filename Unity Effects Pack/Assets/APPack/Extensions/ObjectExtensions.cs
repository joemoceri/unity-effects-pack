namespace APPack
{
    public static class ObjectExtensions
    {
        public static void SetField(this object obj, string fieldName, object newValue)
        {
            var info = obj.GetType().GetField(fieldName);
            if (info != null)
                info.SetValue(obj, newValue);
        }
    }
}
