namespace BarometersBRS1M.Properties {
    
    
    // Этот класс позволяет обрабатывать определенные события в классе параметров:
    //  Событие SettingChanging возникает перед изменением значения параметра.
    //  Событие PropertyChanged возникает после изменения значения параметра.
    //  Событие SettingsLoaded возникает после загрузки значений параметров.
    //  Событие SettingsSaving возникает перед сохранением значений параметров.
    internal sealed partial class Settings {
        
        public Settings() {
            // // Для добавления обработчиков событий для сохранения и изменения параметров раскомментируйте приведенные ниже строки:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Добавьте здесь код для обработки события SettingChangingEvent.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Добавьте здесь код для обработки события SettingsSaving.
        }
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("гПаВх")]
        public global::BarometersBRS1M.UnitOfMeasureInput UnitInput
        {
            get
            {
                return ((global::BarometersBRS1M.UnitOfMeasureInput)(this["UnitInput"]));
            }
            set
            {
                this["UnitInput"] = value;
            }
        }


        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("гПаВых")]
        public global::BarometersBRS1M.UnitOfMeasureOutput UnitOutput
        {
            get
            {
                return ((global::BarometersBRS1M.UnitOfMeasureOutput)(this["UnitOutput"]));
            }
            set
            {
                this["UnitOutput"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Hex")]
        public global::BarometersBRS1M.DataMode DataMode
        {
            get
            {
                return ((global::BarometersBRS1M.DataMode)(this["DataMode"]));
            }
            set
            {
                this["DataMode"] = value;
            }
        }
    }
}
