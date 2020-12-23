/*
 Navicat Premium Data Transfer

 Source Server         : 本地VMWare
 Source Server Type    : MySQL
 Source Server Version : 50610
 Source Host           : 192.168.0.61:3306
 Source Schema         : demobackstage

 Target Server Type    : MySQL
 Target Server Version : 50610
 File Encoding         : 65001

 Date: 19/12/2020 22:46:29
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for menu
-- ----------------------------
DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '菜单名称',
  `rank` int(11) NOT NULL COMMENT '排序',
  `level` int(11) NOT NULL COMMENT '等级',
  `parentid` int(11) NOT NULL COMMENT '父级id',
  `url` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址',
  `isdir` int(11) NOT NULL COMMENT '是否目录',
  `remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of menu
-- ----------------------------
INSERT INTO `menu` VALUES (23, '系统管理', 1, 1, 0, NULL, 1, '系统管理');
INSERT INTO `menu` VALUES (24, '登录日志', 1, 2, 23, '/System/UserLoginLog', 0, '登录日志');
INSERT INTO `menu` VALUES (25, '菜单管理', 2, 2, 23, '/System/Menu', 0, '菜单管理');

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '名称',
  `remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `name`(`name`) USING BTREE COMMENT '角色名称不能重复'
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES (1, '权限管理角色', '菜单管理、角色管理等');

-- ----------------------------
-- Table structure for rolemenu
-- ----------------------------
DROP TABLE IF EXISTS `rolemenu`;
CREATE TABLE `rolemenu`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `roleid` int(11) NOT NULL COMMENT '角色id',
  `menuid` int(11) NOT NULL COMMENT '菜单id',
  `permissions` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '权限',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of rolemenu
-- ----------------------------
INSERT INTO `rolemenu` VALUES (1, 1, 23, '1');
INSERT INTO `rolemenu` VALUES (2, 1, 24, '1,2,3,4');
INSERT INTO `rolemenu` VALUES (4, 1, 25, '1,2,3,4');

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `username` varchar(20) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '用户名',
  `pwd` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT '密码',
  `nickname` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '昵称',
  `regtime` datetime(0) NOT NULL COMMENT '注册时间',
  `regip` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '注册ip',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `username`(`username`) USING BTREE COMMENT '用户名不能重复'
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of userinfo
-- ----------------------------
INSERT INTO `userinfo` VALUES (1, 'Administrator', '94bb483e2df3220571f5a354bbd9954e', 'Administrator', '2020-12-09 02:57:44', '127.0.0.1');
INSERT INTO `userinfo` VALUES (2, 'AAtest001', '94bb483e2df3220571f5a354bbd9954e', 'AAtest001', '2020-12-09 01:10:27', '127.0.0.1');

-- ----------------------------
-- Table structure for userloginlog
-- ----------------------------
DROP TABLE IF EXISTS `userloginlog`;
CREATE TABLE `userloginlog`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `username` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户名',
  `ip` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '登录ip',
  `time` datetime(0) NOT NULL COMMENT '登录时间',
  `agent` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户代理',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 64 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of userloginlog
-- ----------------------------
INSERT INTO `userloginlog` VALUES (1, 'AAtest001', '127.0.0.1', '2020-12-11 20:34:57', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (2, 'AAtest001', '127.0.0.1', '2020-12-11 21:41:00', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (3, 'AAtest001', '127.0.0.1', '2020-12-11 23:47:45', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (5, 'AAtest001', '127.0.0.1', '2020-12-11 23:50:06', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (6, 'AAtest001', '127.0.0.1', '2020-12-12 04:34:40', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (7, 'AAtest001', '127.0.0.1', '2020-12-12 17:27:41', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (8, 'AAtest001', '127.0.0.1', '2020-12-12 19:18:11', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (9, 'AAtest001', '127.0.0.1', '2020-12-12 20:22:01', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (10, 'AAtest001', '127.0.0.1', '2020-12-12 20:34:39', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (11, 'AAtest001', '127.0.0.1', '2020-12-12 20:37:59', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (12, 'AAtest001', '127.0.0.1', '2020-12-12 20:39:28', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (13, 'AAtest001', '127.0.0.1', '2020-12-12 21:53:49', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (14, 'AAtest001', '127.0.0.1', '2020-12-12 22:48:40', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (15, 'AAtest001', '127.0.0.1', '2020-12-13 18:16:02', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (16, 'AAtest001', '127.0.0.1', '2020-12-13 19:47:14', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (17, 'AAtest001', '127.0.0.1', '2020-12-13 21:04:09', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (18, 'AAtest001', '127.0.0.1', '2020-12-13 23:35:40', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (19, 'AAtest001', '127.0.0.1', '2020-12-14 00:36:44', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (20, 'AAtest001', '127.0.0.1', '2020-12-14 01:03:53', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (21, 'Administrator', '127.0.0.1', '2020-12-14 01:11:09', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (22, 'AAtest001', '127.0.0.1', '2020-12-14 01:12:34', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (23, 'Administrator', '127.0.0.1', '2020-12-14 01:21:29', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (24, 'AAtest001', '127.0.0.1', '2020-12-14 01:54:22', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (25, 'AAtest001', '127.0.0.1', '2020-12-14 02:57:53', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (26, 'AAtest001', '127.0.0.1', '2020-12-14 23:35:39', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (27, 'AAtest001', '127.0.0.1', '2020-12-15 16:03:14', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (28, 'AAtest001', '127.0.0.1', '2020-12-15 16:45:25', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (29, 'AAtest001', '127.0.0.1', '2020-12-15 18:12:31', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (30, 'AAtest001', '127.0.0.1', '2020-12-16 15:45:24', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (31, 'AAtest001', '127.0.0.1', '2020-12-16 16:59:50', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (32, 'AAtest001', '127.0.0.1', '2020-12-16 17:19:33', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (33, 'AAtest001', '127.0.0.1', '2020-12-16 19:37:41', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (34, 'AAtest001', '127.0.0.1', '2020-12-16 20:08:42', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (35, 'AAtest001', '127.0.0.1', '2020-12-16 20:39:13', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (36, 'AAtest001', '127.0.0.1', '2020-12-16 23:25:31', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (37, 'AAtest001', '127.0.0.1', '2020-12-16 23:49:36', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (38, 'AAtest001', '127.0.0.1', '2020-12-17 00:14:12', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (39, 'AAtest001', '127.0.0.1', '2020-12-17 00:47:25', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (40, 'AAtest001', '127.0.0.1', '2020-12-17 15:26:30', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (41, 'AAtest001', '127.0.0.1', '2020-12-17 15:35:57', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (42, 'Administrator', '127.0.0.1', '2020-12-17 16:02:29', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (43, 'AAtest001', '127.0.0.1', '2020-12-17 16:03:40', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (44, 'AAtest001', '127.0.0.1', '2020-12-17 16:27:11', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (45, 'AAtest001', '127.0.0.1', '2020-12-17 17:06:09', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (46, 'AAtest001', '127.0.0.1', '2020-12-17 19:55:42', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (47, 'AAtest001', '127.0.0.1', '2020-12-17 21:39:38', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (48, 'AAtest001', '127.0.0.1', '2020-12-17 23:01:13', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (49, 'AAtest001', '127.0.0.1', '2020-12-18 03:11:04', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (50, 'AAtest001', '127.0.0.1', '2020-12-18 15:10:05', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (51, 'AAtest001', '127.0.0.1', '2020-12-18 15:44:17', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (52, 'AAtest001', '127.0.0.1', '2020-12-18 16:23:55', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (53, 'AAtest001', '127.0.0.1', '2020-12-18 17:20:56', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (54, 'AAtest001', '127.0.0.1', '2020-12-18 17:54:31', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (55, 'AAtest001', '127.0.0.1', '2020-12-18 19:13:30', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (56, 'AAtest001', '127.0.0.1', '2020-12-18 20:32:07', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (57, 'AAtest001', '127.0.0.1', '2020-12-18 23:09:22', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (58, 'AAtest001', '127.0.0.1', '2020-12-19 03:18:33', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (59, 'AAtest001', '127.0.0.1', '2020-12-19 14:26:01', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (60, 'AAtest001', '127.0.0.1', '2020-12-19 15:00:09', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (61, 'AAtest001', '127.0.0.1', '2020-12-19 16:21:58', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (62, 'AAtest001', '127.0.0.1', '2020-12-19 16:54:42', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');
INSERT INTO `userloginlog` VALUES (63, 'AAtest001', '127.0.0.1', '2020-12-19 22:44:35', 'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36');

-- ----------------------------
-- Table structure for userrole
-- ----------------------------
DROP TABLE IF EXISTS `userrole`;
CREATE TABLE `userrole`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `userid` int(11) NOT NULL COMMENT '用户id',
  `roleid` int(11) NOT NULL COMMENT '角色id',
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `user`(`userid`, `roleid`) USING BTREE COMMENT '用户和角色不能重复'
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of userrole
-- ----------------------------
INSERT INTO `userrole` VALUES (1, 2, 1);

SET FOREIGN_KEY_CHECKS = 1;
