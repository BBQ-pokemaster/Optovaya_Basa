//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Optovaya_Basa
{
    using System;
    using System.Collections.Generic;
    
    public partial class Postavka
    {
        public int NomerScheta { get; set; }
        public System.DateTime Date { get; set; }
        public int QuanitityOfTovar { get; set; }
        public int Cost { get; set; }
        public int Tovars { get; set; }
        public int Postavchik { get; set; }
    
        public virtual PostavchikiTable PostavchikiTable { get; set; }
        public virtual TovarTable TovarTable { get; set; }
    }
}
