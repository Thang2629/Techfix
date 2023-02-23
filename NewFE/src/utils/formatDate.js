import moment from "moment";

export const formatMDY = (data) => {
	return data ? window.moment.utc(data).local().format("MM/DD/YYYY") : "";
};

export const customFormat = (data, format) => {
	const date = window.moment.utc(data).local();
	return date.isValid() ? date.format(format) : "";
};

export const getDateFormat = (date) => {
	const d = moment(date);
	return date && d.isValid() ? d : "";
};

/**
 *
 * @param {*} data
 * @param {string} type 'day' 'month' year'
 *
 */

export const startDMY = (data, type) => {
	return moment(data).startOf(type).toString();
};

export const endDMY = (data, type) => {
	return moment(data).endOf(type).toString();
};

export const formatMDYWithParam = (param) => {
	return param && param.value ? formatMDY(param.value) : "";
};
export const formatMMDDYYYY = (data) => {
	return data ? moment(data).format("MM/DD/YYYY") : "";
};

export const formatTime = (data) => {
	return data ? moment(data).format("HH:mm A") : "";
};

export const formatFullTime = (data) => {
	return data ? moment(data).format("MM/DD/YYYY hh:mm A") : "";
};

export const exportToChatTime = (isoDate) => {
	if (!isoDate) return;
	const diff = moment(isoDate).diff(new Date(), "hours");

	let result;

	if (-diff < 10) {
		result = window.moment(isoDate).fromNow();
	}

	if (-diff >= 10) {
		result = window.moment(isoDate).calendar();
	}

	return result;
};

export const formattedFullTime = (data) =>
	moment(data).format("YYYY-MM-DDTHH:mm:ss[Z]");

export const startISOTime = (data, typeView) => {
	return moment(data).startOf(typeView).toISOString();
};

export const endISOTime = (data, typeView) => {
	return moment(data).endOf(typeView).toISOString();
};

export const convertToISOTime = (data) => {
	return moment(data).toISOString();
};

export const getPreviousDays = (number) => {
	return moment().subtract(number, "days");
};
export const getPrevious30Days = () => {
	return getPreviousDays(30);
};
