import React, { useState, useEffect } from "react";
import { Table } from "antd";
import * as api from "config/axios";
import { useSelector, useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import "./Grid.less";
import isEmpty from "lodash/isEmpty";
const Grid = ({ urlEndpoint, columns, data, isHidePagination, ...rest }) => {
  const dispatch = useDispatch();
  const searchText = useSelector((state) => state.global.searchText);
  const readGrid = useSelector((state) => state.global.refreshGrid);
  const filterParams = useSelector((state) => state.global.filterParams);
  const [loading, setLoading] = useState(false);
  const [rowData, setRowData] = useState([]);

  const [tableParams, setTableParams] = useState({
    pagination: {
      current: 1,
      pageSize: 10,
      showSizeChanger: false,
    },
  });
  const [selectedIds, setSelectedIds] = useState([]);

  // useEffect(() => {
  //   !isEmpty(urlEndpoint) && fetchData(searchText);
  //   // eslint-disable-next-line react-hooks/exhaustive-deps
  // }, [searchText]);

  // useEffect(() => {
  //   if (readGrid) {
  //     !isEmpty(urlEndpoint) && fetchData();
  //   }
  //   // eslint-disable-next-line react-hooks/exhaustive-deps
  // }, [readGrid]);
  const handlePageNumClick = (page) => {
    fetchData(page);
  };

  useEffect(() => {
    !isEmpty(urlEndpoint) && fetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const resetState = () => {
    dispatch(actions.refreshGrid(false));
  };

  const fetchData = (page = 1) => {
    setLoading(true);
    const paramsRequest = {
      FilterParams: filterParams ? filterParams.FilterParams : [],
      PageSize: tableParams.pagination.pageSize,
      PageNumber: page,
    };
    api
      .sendPost(urlEndpoint, paramsRequest)
      .then((results) => {
        if (results) {
          setRowData(results?.Data); // todo: add params
          setTableParams({
            FilterParams: filterParams.FilterParams,
            pagination: {
              ...tableParams.pagination,
              total: results.TotalCount,
              current: results.CurrentPage,
              pageSize: results.PageSize,
            },
          });
          // setTableParams({
          //   ...tableParams,
          //   pagination: {
          //     ...tableParams.pagination,
          //     total: results.TotalCount,
          //     defaultCurrent: results.CurrentPage,
          //   },
          // });
          if (readGrid) {
            resetState();
          }
        } else {
          setRowData([]);
          setTableParams({
            FilterParams: filterParams.FilterParams,
            pagination: {
              ...tableParams.pagination,
              // 200 is mock data, you should read it from server
              // total: data.totalCount,
            },
          });
        }
      })
      .catch((err) => {
        console.log("err ne >>> ", err);
      })
      .finally(() => {
        setLoading(false);
      });
  };

  // useEffect(() => {
  //   data && setRowData(data);
  //   // eslint-disable-next-line react-hooks/exhaustive-deps
  // }, [JSON.stringify(tableParams)]);

  useEffect(() => {
    if (data) {
      setRowData(data);
      setTableParams({
        FilterParams: tableParams.FilterParams,
        pagination: {
          ...tableParams.pagination,
          total: Math.ceil(data.length / 10),
        },
      });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [data]);

  const onChange = (pagination, filters, sorter, extra) => {
    setTableParams({
      ...tableParams,
      pagination: {
        ...tableParams.pagination,
        pageIndex: pagination.current,
      },
      sorter: { ...sorter },
      searchText: searchText,
    });
  };

  const rowSelection = {
    onChange: (selectedRowKeys, selectedRows) => {
      console.log(
        `selectedRowKeys: ${selectedRowKeys}`,
        "selectedRows: ",
        selectedRows
      );
      setSelectedIds(selectedRowKeys);
    },
    getCheckboxProps: (record) => ({
      disabled: record.name === "Disabled User", // Column configuration not to be checked
      name: record.name,
    }),
  };

  return (
    <div className="grid">
      <Table
        columns={columns}
        dataSource={rowData}
        // onChange={onChange}
        bordered
        // pagination={isHidePagination ? false : tableParams.pagination}
        pagination={{
          onChange: handlePageNumClick,
          current: tableParams.pagination.current,
          total: tableParams.pagination.total,
          pageSize: 10,
          showSizeChanger: false,
        }}
        loading={loading}
        rowKey="id"
        {...rest}
      />
    </div>
  );
};

Grid.propTypes = {};

export default Grid;
