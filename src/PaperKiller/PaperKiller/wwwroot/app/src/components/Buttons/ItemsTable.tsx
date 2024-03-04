import React, { memo, FC } from "react";

import classes from "../../styles/Tables/MyItemsTable.module.css";
import resets from "../../styles/_resets.module.css";
import { MyItemsDTO } from "../../interfaces/MyItemsDTO";

interface Props {
    className?: string;
    classes?: {
        tableGroup?: string;
    };
    items: MyItemsDTO[];
}

const ItemsTable: FC<Props> = memo(function MyItemsTable({ items, ...props }) {
	return (
		<div className={`${resets.storybrainResets} ${classes.root}`}>
			<div className={`${props.classes?.tableGroup || ""} ${classes.tableGroup}`}>
				<div className={classes.frame27}>
					<div className={classes.unnamed}>Название</div>
				</div>
				<div className={classes.frame26}>
					<div className={classes.unnamed2}>Серийный  номер</div>
				</div>
				<div className={classes.frame28}>
					<div className={classes.unnamed3}>Статус</div>
				</div>
				{items.map((item, index) => (
					<React.Fragment key={index}>
						<div className={classes.unnamed4}>Стул</div>
						<div className={classes.serialNumber}>
							{item.chairSerialNumber ? item.chairSerialNumber : "-"}
						</div>
						<div className={classes.status}>
							{item.chairSerialNumber ? "У студента" : "На складе"}
						</div>

						<div className={classes.unnamed5}>Стол</div>
						<div className={classes.serialNumber2}>
							{item.tablesSerialNumber ? item.tablesSerialNumber : "-"}
						</div>
						<div className={classes.status2}>
							{item.tablesSerialNumber ? "У студента" : "На складе"}
						</div>

						<div className={classes.unnamed6}>Полка</div>
						<div className={classes.serialNumber3}>
							{item.shelfSerialNumber ? item.shelfSerialNumber : "-"}
						</div>
						<div className={classes.status3}>
							{item.shelfSerialNumber ? "У студента" : "На складе"}
						</div>

						<div className={classes.unnamed7}>Шкаф</div>
						<div className={classes.serialNumber4}>
							{item.wardrobeSerialNumber ? item.wardrobeSerialNumber : "-"}
						</div>
						<div className={classes.status4}>
							{item.wardrobeSerialNumber ? "У студента" : "На складе"}
						</div>
					</React.Fragment>
				))}
				<div className={classes.line3}></div>
				<div className={classes.line4}></div>
				<div className={classes.line5}></div>
			</div>
		</div>
	);
});


export default ItemsTable;
