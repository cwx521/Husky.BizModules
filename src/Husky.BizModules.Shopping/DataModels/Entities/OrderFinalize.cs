using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Husky.BizModules.Shopping.DataModels
{
	public class OrderFinalize
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int OrderId { get; set; }

		public int? WithdrawalId { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal TotalPaid { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal TotalRefunded { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal AddtionalTip { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		public decimal ServiceFee { get; set; }


		[DefaultValueSql("getdate()"), NeverUpdate]
		public DateTime FinalizedTime { get; set; } = DateTime.Now;


		// Order

		public Order? Order { get; set; }
		public ShopProfitWithdrawal? Withdrawal { get; set; }


		// Calculation

		public decimal WithdrawableAmount => TotalPaid + AddtionalTip + TotalRefunded - ServiceFee;
	}
}
