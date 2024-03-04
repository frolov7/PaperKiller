import React, { memo, FC } from "react";
import { MyItemsDTO } from "../../interfaces/MyItemsDTO";
import classes from "../../styles/Tables/MyLinenTable.module.css";
import resets from "../../styles/_resets.module.css";

interface Props {
    className?: string;
    classes?: {
        tableGroup?: string;
    };
    items: MyItemsDTO[];
}

const LinenTable: FC<Props> = memo(function MyItemsTable({ items, ...props }) {
	return (
		<div className={`${resets.storybrainResets} ${classes.root}`}>
			<div className={`${props.classes?.tableGroup || ""} ${classes.tableGroup}`}>
				<div className={classes.frame24}>
					<div className={classes.unnamed}>Название</div>
				</div>
				<div className={classes.frame23}>
					<div className={classes.unnamed2}>Серийный  номер</div>
				</div>
				<div className={classes.frame22}>
					<div className={classes.unnamed3}>Статус</div>
				</div>
				{items.map((item, index) => (
					<React.Fragment key={index}>
						<div className={classes.unnamed4}>Простыня</div>
						<div className={classes.serialNumber}>
							{item.bedsheetSerialNumber ? item.bedsheetSerialNumber : "-"}
						</div>
						<div className={classes.status}>{item.bedsheetSerialNumber ? "У студента" : "На складе"}</div>

						<div className={classes.unnamed5}>Наволочка</div>
						<div className={classes.serialNumber2}>
							{item.bedspreadSerialNumber ? item.bedspreadSerialNumber : "-"}
						</div>
						<div className={classes.status2}>{item.bedspreadSerialNumber ? "У студента" : "На складе"}</div>

						<div className={classes.unnamed6}>Пододеяльник</div>
						<div className={classes.serialNumber3}>
							{item.pillowcaseSerialNumber ? item.pillowcaseSerialNumber : "-"}
						</div>
						<div className={classes.status3}>{item.pillowcaseSerialNumber ? "У студента" : "На складе"}</div>

						<div className={classes.unnamed7}>Покрывало</div>
						<div className={classes.serialNumber4}>
							{item.duvetSerialNumber ? item.duvetSerialNumber : "-"}
						</div>
						<div className={classes.status4}>{item.duvetSerialNumber ? "У студента" : "На складе"}</div>

						<div className={classes.unnamed8}>Полотенце</div>
						<div className={classes.serialNumber5}>
							{item.towelSerialNumber ? item.towelSerialNumber : "-"}
						</div>
						<div className={classes.status5}>{item.towelSerialNumber ? "У студента" : "На складе"}</div>
					</React.Fragment>
				))}
				<div className={classes.line3}></div>
				<div className={classes.line4}></div>
				<div className={classes.line5}></div>
			</div>
		</div>
	);
});


export default LinenTable;
