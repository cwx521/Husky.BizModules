namespace Husky.BizModules.Shopping.DataModels
{
	public enum RefundReason
	{
		[Label("取消订单")]
		CancelOrder,

		[Label("退运费")]
		ReturnExpressFee,

		[Label("收到商品破损")]
		ProductBroken,

		[Label("商品漏发")]
		ProductMissing,

		[Label("商品错发")]
		ProductNoMatch,

		[Label("商品需要维修")]
		ProductNeedsRepair,

		[Label("商品质量问题")]
		ProductQualityIssue,

		[Label("收到商品与描述不符")]
		ProductNoExpected,

		[Label("未按约定时间发货")]
		NotDelivered,

		[Label("发票问题")]
		InvoiceIssue,

		[Label("其它")]
		Others = 99
	}
}
