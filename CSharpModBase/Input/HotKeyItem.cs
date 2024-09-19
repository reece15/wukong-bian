﻿namespace CSharpModBase.Input
{
    public class HotKeyData(ModifierKeys modifiers, Key key)
    {
        public static int GetCode(ModifierKeys modifiers, int vk)
        {
            return ((int)modifiers << 16) | vk;
        }

        public static int GetCode(ModifierKeys modifiers, Key key)
        {
            return ((int)modifiers << 16) | (int)key;
        }

        public HotKeyData() : this(ModifierKeys.None, Key.None) {}

        public ModifierKeys Modifiers { get; set; } = modifiers;
        public Key Key { get; set; } = key;
        public string KeyString => KeyUtils.KeyToString(Modifiers, Key);
        public int Code => GetCode(Modifiers, (int)Key);

        public void SetFromCode(int code)
        {
            Modifiers = (ModifierKeys)(code >> 16);
            Key = (Key)(code & 0xFF);
        }

        public void SetKey(ModifierKeys modifiers, Key key)
        {
            Modifiers = modifiers;
            Key = key;
        }

        public override string ToString()
        {
            return KeyString;
        }
    }


    public class HotKeyItem : HotKeyData
    {
        public string Label { get; set; }
        public Action Action { get; set; }
        /// <summary>
        /// 0为不支持连按，大于0为连按触发毫秒间隔
        /// </summary>
        public int RepeatMs { get; set; }
        /// <summary>
        /// 上一次触发的时间，毫秒
        /// </summary>
        public long LastTriggerMs { get; set; }
        /// <summary>
        /// 正在被按下
        /// </summary>
        public bool IsPressed { get; set; }
        public bool RunOnGameThread { get; set; } = true;

        public HotKeyItem WithKey(ModifierKeys Modifiers, Key Key)
        {
            return new(Label, Modifiers, Key, Action);
        }

        public HotKeyItem(ModifierKeys modifiers, Key key, Action action) : this("", modifiers, key, action)
        {
        }

        public HotKeyItem(string label, ModifierKeys modifiers, Key key, Action action) : base(modifiers, key)
        {
            Label = label;
            Action = action;
        }

        public HotKeyItem() : this("", ModifierKeys.None, Key.None, EmptyAction)
        {
        }

        private static void EmptyAction()
        {
        }
    }
}
