/* eslint-disable */
//import * as jQuery from 'jquery';
//import {awef} from './awef.js';
//import {awe} from './awe.js';

var awem = function ($) {
    var autoSearchTresh = 5;
    var loop = awef.loop, qsel = awef.select, encode = awef.encode, isNull = awef.isNull, isNotNull = awef.isNotNull, isEmp = awef.isNullOrEmp;
    var addDestrFunc = awe.addDestr, bind = awe.bind, unbind = awe.unbind, guc = awe.guc, outerh = awef.outerh, outerw = awef.outerw, len = awef.len, hovOpen = awe.hovOpen;

    var afs = awe.fs;
    var trg = afs.trg, istrg = afs.istrg;

    var minZindex = 1;
    var popupDraggable = 1;
    var maxLookupDropdownHeight = 365;
    var maxDropdownHeight = 420;
    var menuMinh = 200;
    var comboMinh = 100;
    var cacheLimit = 1000;
    var popSpace = 50;
    var popTopSpace = 0;
    var clickOutSpace = 35;
    var hpSpace = popSpace / 2;
    //var closePopOnOutClick = 0;
    var $doc = $(document);
    var $win = $(window);
    var keyEnter = 13;
    var keyRight = 39;
    var keyLeft = 37;
    var keySpace = 32;
    var keyUp = 38;
    var keyDown = 40;
    var keyEsc = 27;
    var keyTab = 9;
    var keyBackspace = 8;
    var keyShift = 16;
    var keyCtrl = 17;

    // keys you can type without opening menu dropdown
    // left arrow, ..., pgup, pgdn, end, home
    var nonOpenKeys = [keyEnter, keyEsc, keyShift, keyLeft, keyRight, keyTab, keyCtrl, 33, 34, 35, 36]; // keys that won't open the menu

    var updownKeys = [keyUp, keyDown];

    // down and up arrow, enter, esc, shift //, left arrow, right arrow
    var controlKeys = [keyUp, keyDown, keyEnter, keyEsc, keyShift];

    var nonComboSearchKeys = updownKeys.concat(nonOpenKeys);

    var isMobile = function () { return awem.isMobileOrTablet(); };

    var sawefocus = 'awe-focus';
    var saweload = 'aweload';
    var sawebeginload = 'awebeginload';
    var sawecolschange = 'awecolschanged';
    var saweinit = 'aweinit';
    var sawerowch = 'awerowch';
    var saweinl = 'aweinline';
    var saweinlbfsave = saweinl + 'bfsave';
    var saweinlsave = saweinl + 'save';
    var saweinlinv = saweinl + 'invalid';
    var saweinledit = saweinl + 'edit';
    var saweinlcancel = saweinl + 'cancel';
    var sawerowc = '.awe-row';
    var saweselected = 'awe-selected';
    var sawecollapsed = 'awe-collapsed';
    var saweselectedc = '.' + saweselected;
    var sawegridcls = '.awe-grid';    
    var sawecontentc = '.awe-content';
    var sddpOutClEv = 'click.ddp mousedown.ddp touchstart.ddp';
    var sfocus = 'focus';
    var smousemove = 'mousemove';
    var soddDocClEv = 'mouseup touchstart.awenpd keydown';
    var sdisabled = 'disabled';
    var sheight = 'height';
    var sminw = 'min-width';
    var se = '';
    var sclick = 'click';
    var schange = 'change';
    var skeyup = 'keyup';
    var skeydown = 'keydown';
    var sposition = 'position';
    var snewrow = 'o-glnew';
    var sglrow = 'o-glrow';
    var schked = 'o-chked';
    var sglrowc = '.' + sglrow;
    var szindex = 'z-index';
    var swidth = 'width';
    var smaxh = 'max-height';
    var sselected = 'selected';
    var loadingHtml = rdiv('awe-loading', '<span/>');
    var sclosespan = '<span class="o-cls" aria-label="close">&times;</span>';
    var scaret = '<div class="o-slbtn"><i class="o-caret"></i></div>';
    var soldngp = 'o-ldngp';
    var soldngpc = '.' + soldngp;
    var snbsp = '&nbsp;';
    var svalc = '.o-v';
    var sgvalidmsg = 'o-gvalidmsg';
    var sawehighlight = 'awe-highlight';

    var cache = {};
    var kvIdOpener = {};
    var tabbable = awe.tabbable;
    var identity = 0;

    $(function () {
        if (minZindex === 1) {
            var nav = $('.navbar-fixed-top:first');
            if (nav.length) {
                minZindex = nav.css(szindex);
            }
        }
    });

    function removeClass(o, s) {
        return o.removeClass(s);
    }

    function addClass(o, s) {
        return o.addClass(s);
    }

    var regDragDrop = awe.rdd;

    //#region awem sync
    function initWave() {
        if ($doc.data('awewave')) return;
        $doc.data('awewave', 1);
        $(function () {
            $doc.on('mousedown touchstart.awenpd', '.awe-btn, .awe-tab-btn, .o-wavs, .awe-sortable.awe-hc, .awe-sortable.awe-col, .o-chk', function (e) {
                var time = 700;
                var btn = $(this);
                if (btn.is(':disabled') || btn.closest('.awe-disabled').length || btn.closest('.nowave').length) return;

                var wc = $('<div class="o-wavc" tabindex="-1"/>');
                var wav = $('<div class="o-wav" tabindex="-1"/>');
                wc.append(wav);

                if (isMobile()) {
                    wc.addClass('o-mobl');
                }

                var csize;
                var istouch = e.type !== 'mousedown';
                var isCol = 0;

                if (istouch) {
                    btn.data('awewvtch', 1);
                    setTimeout(function () { btn.data('awewvtch', 0); }, 330);
                }

                if (btn.data('awewvtch') && !istouch) return;

                var startCoords = istouch ? e.originalEvent.touches[0] : e;

                var x = startCoords.pageX - btn.offset().left;
                var y = startCoords.pageY - btn.offset().top;
                var w = outerw(btn);
                var h = outerh(btn);

                if (btn.is('.o-chk')) {
                    time = 350;
                    csize = w = h = 57;
                    x = y = csize / 2;
                    var marg = csize / 2 - outerw(btn) / 2;
                    wc.css('top', -marg);
                    wc.css('left', -marg);
                }

                // ff col click
                if (btn.is('.awe-sortable .awe-col')) {
                    btn = btn.parent();
                    isCol = 1;
                }

                mouseup();

                if (btn.closest('.awe-groupable').length) {
                    var uprls;
                    var moved;
                    $doc
                        .one('mouseup touchend',
                            function () {
                                uprls = 1;
                            })
                        .one('mousemove touchmove', function () {
                            moved = 1;
                        });

                    setTimeout(function () {
                        if (!uprls && moved) {
                            wc.remove();
                        }
                    },
                        200);
                }

                function mouseup() {
                    wc.width(w).height(isCol ? '100%' : h);
                    var size = Math.max(w, h);

                    wav.css('left', x);
                    wav.css('top', y);
                    wav.width(20).height(20);

                    btn.append(wc);
                    size = csize || Math.max(size * 2, 100);
                    wav.width(size).height(size);

                    wav.css('opacity', 0);
                    setTimeout(function () {
                        wc.remove();
                    }, time);
                }
            });
        });
    }

    function ifnul(val, dval) {
        return isNull(val) ? dval : val;
    }

    function ypos(o) {
        return o.offset().top;
    }

    function scrollTopBy(con, val) {
        con.scrollTop(con.scrollTop() + val);
    }

    function contains(key, keys) {
        return keys.indexOf(key) > -1;
    }

    function outerhn(sel, m) {
        return sel.length ? outerh(sel, m) : 0;
    }

    function which(e) {
        return e.which;
    }

    function isPosFixed(elm) {
        if (!elm || !elm.length || elm.is('body')) return 0;

        if (elm.css('position') === 'fixed') {
            return 1;
        }

        return isPosFixed(elm.parent());
    }

    function getZIndex(el) {
        var val = el.css(szindex);
        if (val && val > 0) return val;
        var parent = el.parent();
        return parent && !parent.is($('body')) ? getZIndex(parent) : 0;
    }

    function calcZIndex(zIndex, el) {
        if (zIndex < minZindex) zIndex = minZindex;
        var zi = getZIndex(el);
        if (zi && zi > zIndex) {
            zIndex = zi;
        }

        return zIndex;
    }

    function slist(cont, opt) {
        var itemsel = opt.sel;
        var onenter = opt.enter;
        var focuscls = opt.fcls || sfocus;
        var selcls = opt.sc || sselected;
        var afs = opt.afs;

        function visf(sel) {
            return sel.filter(':visible').first();
        }

        function focus(item) {
            remFocus();
            item.addClass(focuscls);
        }

        function remFocus() {
            cont.find('.' + focuscls).removeClass(focuscls);
        }

        function scrllTo(to, cangomid, noWinScroll) {
            if (!to.length || !to.is(':visible')) return;

            var ty = ypos(to);
            var th = outerh(to);
            var conh = cont.height();
            var miny = ypos(cont);
            var maxy = miny + conh - th;

            var scrCont = cont;
            var winmax = $win.height() + $doc.scrollTop() - th;
            var winmin = $doc.scrollTop();

            if (!noWinScroll) {
                if (maxy > winmax && winmax < ty) {
                    maxy = winmax;
                    scrCont = $win;
                }

                if (miny < winmin && winmin > ty) {
                    miny = winmin;
                    scrCont = $win;
                }
            }

            var delta = ty < miny ? ty - miny : ty > maxy ? ty - maxy : 0;

            // +1 for ie and ff 
            if (cangomid && delta > th + 1 && scrCont !== $win) {
                delta += conh / 2;
            }

            scrollTopBy(scrCont, delta);
        }

        function scrollToFocused(cangomid, noWinScroll) {
            scrllTo(cont.find('.' + focuscls), cangomid, noWinScroll);
        }

        function autofocus($itemToFocus, noWinScroll) {
            if ($itemToFocus) {
                focus($itemToFocus);
            } else {
                var $selected = cont.find('.' + selcls + ':visible');
                if ($selected.length === 1) {
                    focus($selected);
                } else {
                    while (1) {
                        if (afs) {
                            var safs = visf(cont.find(afs));
                            if (safs.length) {
                                focus(safs);
                                break;
                            }
                        }

                        focus(visf(cont.find(itemsel)));
                        break;
                    }
                }
            }

            scrollToFocused(1, noWinScroll);
        }

        function handleMoveSelectKeys(e) {
            var key = which(e);

            var focused = gfocus();

            var select = function (item, f) {
                if (!focused.length) {
                    autofocus();
                }
                else if (item.length) {
                    focus(item);
                    scrollToFocused();
                } else if (f) {
                    f();
                }
            };

            if (contains(key, controlKeys)) {
                if (key === keyDown) {
                    prevDef(e);
                    select(visf(focused.nextAll(itemsel)), opt.botf);
                } else if (key === keyUp) {
                    prevDef(e);
                    select(visf(focused.prevAll(itemsel)), opt.topf);
                } else if (key === keyEnter) {
                    if (onenter) {
                        onenter(e, focused);
                    }
                    else {
                        var alink = focused.find('a');
                        if (len(alink)) {
                            prevDef(e);
                            alink[0].click();
                        }
                        else {
                            prevDef(e);
                            focused.click();
                        }
                    }
                }

                return 1;
            }

            return 0;
        }

        function gfocus() {
            return cont.find('.' + focuscls);
        }

        return {
            gfocus: gfocus,
            focus: focus,
            scrollToFocused: scrollToFocused,
            scrollTo: scrllTo,
            keyh: handleMoveSelectKeys,
            autofocus: autofocus,
            remf: remFocus
        };
    }

    function dragAndDrop(opt) {
        var dropContainers = [];
        var dropFuncs = [];
        var dropHoverFuncs = [];

        loop(opt.to, function (val) {
            dropContainers.push(val.c);
            dropHoverFuncs.push(val.hover);
            dropFuncs.push(val.drop);
        });

        var dropConts = opt.dropConts;

        if (opt.dropSel || dropConts) {
            opt.tof = function () {
                if (!opt.dropSel)
                    return dropConts;

                if (!dropConts)
                    return $(opt.dropSel).map(function (i, el) { return $(el); }).get();

                var res = [];
                loop(dropConts, function (dc) {
                    res = res.concat(dc.find(opt.dropSel).map(function (i, el) { return $(el); }).get());
                });

                return res;
            };
        }

        var cancelFunc = opt.cancel || function (cx) {
            var dragItm = $(cx.e.target).closest('.o-dragItm');
            if (len(dragItm) && !dragItm.is(cx.sel)) return true;
        };

        return regDragDrop(opt.from, opt.sel, dropContainers, dropFuncs, opt.dragClass, opt.hide, dropHoverFuncs, opt.end,
            opt.reshov, opt.scroll, opt.wrap, opt.ch, cancelFunc, opt.kdh, opt.move, opt.hover, opt.drop, opt.handle, opt.gscroll, opt.tof);
    }

    function dragReor(opt) {
        var placeh;
        var plhclss = opt.plh || 'o-plh';
        var sel = opt.sel;
        var handle = opt.handle;
        var lastItemHovered;
        var fromCont = opt.from;
        var previ;
        var ondrop = opt.ondrop || opt.onDrop;
        var noPlaceh = opt.noPlh;
        // new obj func, obj to be dropped
        var dropElmFunc = opt.newo;
        var showPlacehOnHover;
        var justmoved;
        var stickyPlh = opt.splh;
        var hovHighl = !opt.noHovHighl;

        // func when hov cont but not any item
        var hovec = opt.hovec;

        var swrap = opt.swrap;

        // func on start hover
        var shov = opt.shov;

        //if (stickyPlh) {
        //    hovec = hovec || emptyFunc;
        //}

        // used with sticky placeholder,
        // when hovering a bigger cont/window, but dropping on a predefined cont
        var getHoverOnCont = opt.gcon || function (cx) {
            return cx.cont;
        };

        function wrap(cx) {
            var clone = swrap && swrap(cx);
            var dragObj = cx.drgo;
            previ = dragObj.index();
            cx.previ = previ;
            placeh = dragObj.clone().addClass(plhclss);
            cx.plh = placeh;

            showPlacehOnHover = 1;
            justmoved = 0;
            placeh.hide();
            dragObj.after(placeh);

            return clone || dragObj.clone();
        }

        function reshov(cx) {
            if (placeh && !stickyPlh) {
                placeh.detach();
            }

            var cont = cx.itmCont || cx.cont;

            if (cont) {
                removeClass(cont, 'o-dragForb ' + sawehighlight);
            }

            opt.reshov && opt.reshov(cx);

            lastItemHovered = null;
        }

        // executed when hovering opt.to
        // returns the cont parameter used in gscroll
        function hoverFunc(cx) {
            shov && shov(cx);

            //var hoverf = opt.hover;
            //if (hoverf) {
            //    return hoverf(cx);
            //}

            placeh = cx.plh;
            var cont = getHoverOnCont(cx), x = cx.x, y = cx.y;

            cx.itmCont = cont;

            if (hovHighl) {
                addClass(cont, sawehighlight);
            }

            if (showPlacehOnHover) {
                placeh.show();
                showPlacehOnHover = 0;
            }

            var sogroup = 'data-ogroup';

            var contGroups = cont.attr(sogroup);
            cx.cancel = 0;
            if (contGroups) {
                contGroups = contGroups.split(',');
                var itmGroup = cx.drgo.attr(sogroup);
                if (!contains(itmGroup, contGroups)) {
                    cx.cancel = 1;
                    addClass(cont, 'o-dragForb');
                }
            }

            if (opt.noReorder || noPlaceh) {
                if (!noPlaceh) { cont.append(placeh); }
                return cont;
            }

            /*if (opt.chkhov && !opt.chkhov(cx)) return cont;*/

            var itemHovered = 0;
            var elms = cont.find(sel + ':visible').get();

            // check still hovering last
            if (isNotNull(lastItemHovered)) {
                var ofs = lastItemHovered.offset();
                var lx = ofs.left;
                var ly = ofs.top;

                if (ly + outerh(lastItemHovered) > y &&
                    ly < y &&
                    lx + outerw(lastItemHovered) > x
                    && lx < x) {
                    return cont;
                }
            }

            var elmsLen = elms.length;
            var cof = cont.offset();
            var minDist;
            var isAbove = y < cof.top;
            var outside =
                isAbove || x < cof.left ||
                y > cof.top + outerh(cont) || x > cof.left + outerw(cont);

            if (outside && !stickyPlh) {
                return cont;
            }

            for (var i = 0; i < elmsLen; i++) {
                var item = $(elms[i]);
                var iof = item.offset();
                var iow = outerw(item);
                var ioh = outerh(item);

                var ix = iof.left + iow;
                var iy = iof.top + ioh;

                if (isAbove) {
                    var distance = Math.abs(x - (ix - iow / 2)) + Math.abs(y - (iy - ioh / 2));

                    if (!i || distance < minDist) {
                        minDist = distance;
                        lastItemHovered = item;
                        itemHovered = item;
                    }
                } else {
                    if (y < iy && x < ix) {
                        lastItemHovered = item;
                        itemHovered = item;
                        break;
                    }
                }
            }

            if (!itemHovered && outside) {
                for (var j = elmsLen - 1; j >= 0; j--) {
                    var itm = $(elms[j]);
                    var jof = itm.offset();
                    var jx = jof.left;
                    var jy = jof.top;

                    if (x > jx && y > jy) {
                        lastItemHovered = itm;
                        itemHovered = itm;
                        break;
                    }
                }
            }

            if (justmoved) {
                if (!itemHovered) {
                    justmoved = 0;
                } else if (justmoved.is(itemHovered)) {
                    return cont;
                }
            }

            var st = $win.scrollTop();

            if (itemHovered) {
                justmoved = itemHovered;

                var pi = placeh.index();

                if (!len(placeh.closest(cx.cont))) {
                    // hovering different cont
                    pi = -1;
                }

                if (itemHovered.index() < pi || pi === -1) {
                    itemHovered.before(placeh);
                } else {
                    itemHovered.after(placeh);
                }
            } else {
                if (hovec) {
                    hovec(cx);
                } else {
                    if (!placeh.parent().is(cont)) {
                        cont.append(placeh);
                    }
                }
            }

            // chrome page jump
            if (st !== $win.scrollTop()) {
                $win.scrollTop(st);
            }

            return cont;
        }

        function dropFunc(cx) {
            if (cx.cancel) return;
            var dragObj = cx.drgo;
            var dropf = opt.drop || opt.dropFunc;
            if (dropf) return dropf(cx);
            var nobj = dragObj;
            if (dropElmFunc) {
                nobj = dropElmFunc(cx);
            }

            cx.dropElm = nobj;
            //cx.placeh = placeh;

            if (placeh.closest('body').length) {
                if (opt.onFakeDrop) {
                    cx.noSrcShow = 1;
                }
                else {
                    placeh.after(nobj).remove();
                }
            }

            nobj.trigger('awedrop', { from: fromCont, previ: previ });
            opt.onFakeDrop && opt.onFakeDrop(cx);
            ondrop && ondrop(cx);
        }

        // get additional containers to scroll on drag
        function gscroll(cont) {
            if (cont) {
                var scrollCont = cont.closest('.o-scrollCont');
                if (len(scrollCont)) {
                    return [{ c: scrollCont, y: 1 }];
                }

                return [{ c: cont, y: 1 }];
            }
        }

        function end(cx) {
            //cx.cont null when dragstart without dragmove
            if (cx.cont && stickyPlh && len(placeh.closest('body'))) {
                dropFunc(cx);
            }

            placeh.remove();
            placeh = null;
        }

        return dragAndDrop({
            from: opt.from,
            sel: sel,
            handle: handle,
            dropSel: opt.to,
            dragClass: opt.dragClass,
            tof: opt.tof,
            dropConts: opt.dropConts,
            wrap: wrap,
            hover: hoverFunc,
            drop: dropFunc,
            reshov: reshov,
            cancel: opt.cancel,
            gscroll: opt.gscroll || gscroll,
            end: end,
            hide: ifnul(opt.hide, 1), // hide dragObj until dropped
            scroll: opt.scroll || [{ c: $win, y: 1 }]
        });
    }

    function isChildPopup(it, mId) {
        var pop1 = it.closest('.o-pu');
        if (pop1.data('pid') === mId) {
            return 1;
        }
        var pid, mclick = 0;
        if (it.is('.o-pmodal')) {
            mclick = 1;
        }

        if (pop1.length) {
            pid = pop1.data('i');
        }

        if (pid) {
            if (pid === mId && !mclick) return 1;

            var popener = kvIdOpener[pid];
            if (popener)
                return isChildPopup(popener, mId);
        }
    }

    function dropdownPopup(o) {
        var outsideClickClose = readTag(o, "Occ", o.outClickClose);
        var isDropDown = readTag(o, "Dd", o.dropdown);
        var showHeader = readTag(o, "Sh", !isDropDown);
        var toggle = readTag(o, "Tg");

        var pp = o; // popup properties
        var popup = pp.d; // popup div
        var itmoved, header, $opener, openerId, fls, popt, top;
        var host = $('body');

        pp.id = pp.id || se;
        o.cx = {};
        var cx = o.cx;
        var api = function () { };
        o.cx.api = api;

        var modal;
        var wrap;

        var pmccls = ' o-pmc o-pu ' + (pp.popupClass || se);
        if (isDropDown) pmccls += ' o-ddp';
        
        if (o.pwrp) {
            wrap = popup.closest('.o-pwrap');
            modal = wrap.find('.o-pmodal');
        }
        else {
            wrap = $(rdiv('o-pwrap', rdiv(pmccls, se, 'tabindex="-1"'))).hide();
            modal = $(rdiv('o-pmodal o-pu', se, 'tabindex="-1" data-i="' + pp.id + '"'))
        }

        wrap.find('.o-pu').first().attr('data-i', pp.id);

        var $dropdownPopup = wrap.find('.o-pmc');

        if (!o.pwrp) {
            header = $(rdiv('o-phdr', rdiv('o-ptitl', pp.title || snbsp) + sclosespan));
            $dropdownPopup.prepend(header);
            $dropdownPopup.append(popup);
        } else {
            header = $dropdownPopup.find('.o-phdr');
        }

        var btns = pp.btns;

        

        var sopener = o.opener;

        pp.mlh = 0;

        !o.menu && popup.addClass('o-pc');

        if (!isDropDown) {
            popup.addClass('o-fpp');
        }

        if (isNull(pp.minw)) {
            popup.css(sminw, pp.minw);
        }

        if (o.rtl) {
            $dropdownPopup.addClass('awe-rtl').css('direction', 'rtl');
        }


        modal.on(skeyup, closeOnEsc);

        $dropdownPopup.on(skeydown,
            function (e) {
                if (e.keyCode === keyTab) {
                    var tabbables = tabbable($dropdownPopup),
                        first = tabbables.first(),
                        last = tabbables.last();
                    var tg = trg(e);
                    if (tg.is(last) && !e.shiftKey) {
                        first.focus();
                        return false;
                    } else if (tg.is(first) && e.shiftKey) {
                        last.focus();
                        return false;
                    }
                }
            });

        var isFixed;
        var zIndex = minZindex;

        function layPopup(isResize, canShrink) {

            var initSt = popup.scrollTop();

            if (pp.nolay) return;

            if (isResize) {
                // reset position changed by dragging popup
                itmoved = 0;
            }

            if (!cx.isOpen) return;

            var winavh = $win.height() - popSpace;
            var winavw = $win.width() - popSpace;

            if (top) {
                winavh -= popTopSpace;
            }

            modal.css(szindex, zIndex);
            $dropdownPopup.css('overflow-y', 'auto');
            if (zIndex) {
                $dropdownPopup.css(szindex, zIndex);
            }

            popup.css(swidth, se);
            popup.css(sheight, se);
            popup.css(smaxh, se);

            var oapi = o.api || {};

            if (oapi.rlay) {
                oapi.rlay();
            }

            var capHeight = o.f ? outerhn(o.f.find('.awe-openbtn:first'), 1) : 0;

            fls = pp.fullscreen;
            top = pp.top;

            if (openerId && !$opener.closest(document).length) {
                $opener = $('#' + openerId);
            }

            var height = pp.dh || pp.height;

            if (!height && !o.menu) {
                height = Math.max(350, outerhn($dropdownPopup));
            }

            var maxph = 0;

            var popoutw = outerw($dropdownPopup) - outerw(popup);
            var popouth = outerh($dropdownPopup) - outerh(popup);

            if (o.lkp) {
                height = pp.dh || maxLookupDropdownHeight;
                maxph = pp.dh || maxLookupDropdownHeight;
            }

            var minw = popt.minw || 0;
            if (o.menu) {
                maxph = maxDropdownHeight;

                if (len($opener) && !popt.xy) {
                    minw = Math.max(outerw($opener), minw);
                }

                popup.css(sminw, minw);

                canShrink = false;
            }

            var limw = winavw;
            if (pp.maxw) {
                popup.css('max-width', pp.maxw);
                limw = pp.maxw;
            }

            if (pp.width) {
                if (!isDropDown || !pp.srv) {
                    minw = Math.min(pp.width, Math.min(limw, winavw)) - popoutw;
                    popup.css(sminw, minw);
                }
            }
            else {
                // set minw based on opener
                if (len($opener)) {
                    popup.css(sminw, outerw($opener));
                }
            }

            var minh = height;
            if (!isDropDown || pp.hws) {
                if (pp.height) {
                    minh = pp.height;
                    if (height < minh) height = minh;
                    if (maxph < minh) maxph = minh;
                    popup.css('min-height', Math.min(pp.height, winavh) - popouth);
                }
            }

            function chkfulls(ph) {
                var pw = outerw($dropdownPopup);
                var h = outerh($dropdownPopup);
                var wlim = 25, hlim = 70;

                if (pp.af || pp.menu) {
                    wlim = 200;
                    hlim = 300;

                    h = Math.max(ph, h);
                }

                if (isNotNull(pp.wlim)) {
                    wlim = pp.wlim;
                }

                var hcondit = pw > winavw - wlim && h > winavh - hlim;

                if (!o.lkp) {
                    hcondit = hcondit && h * .7 > winavh - h;
                }

                if (hcondit) {
                    fls = 1;
                }

                if (fls) {
                    var avh = winavh - popouth - (showHeader || btns ? 0 : clickOutSpace);
                    if (o.lkp) {
                        o.avh = avh;
                        o.nph = popouth;
                    }

                    popup.css(swidth, winavw - popoutw);

                    // set max-height for dropdown popups (menu)
                    popup.css(o.menu ? smaxh : sheight, avh);
                }

                if (fls || pp.modal) {
                    modal.show();
                } else {
                    modal.hide();
                }

                return fls;
            }

            function setMaxHeight(poph, maxh) {
                var avh = maxh - popouth;

                popup.css(smaxh, avh);

                if (o.lkp) {
                    avh = poph - popouth;

                    popup.css(sheight, avh);

                    o.avh = avh;
                    o.nph = popouth;
                }
            }

            function setHeight(poph, maxh, valign) {
                if (valign === 'top') {
                    popup.css(sheight, poph);
                } else {
                    var h = maxh;
                    if (valign !== 'fls') {
                        h = Math.min(h, maxDropdownHeight); // maxpoph
                    }

                    h && popup.css(smaxh, h);
                }
            }

            layDropdownPopup2(o,
                $dropdownPopup,
                isFixed,
                capHeight,
                isDropDown ? $opener : null,
                o.menu ? setHeight : setMaxHeight,
                itmoved,
                canShrink,
                chkfulls,
                o.menu ? menuMinh : minh, //minh,
                height,
                maxph,
                popup,
                popt,
                top);

            popup.scrollTop(initSt);
            popup.trigger('awepos');
        }

        function outClickClose(e) {
            var shouldClose;
            if (isNotNull(outsideClickClose)) {
                shouldClose = outsideClickClose;
            } else {
                shouldClose = $opener && isDropDown; //closePopOnOutClick ||
            }

            if (shouldClose) {
                var tg = trg(e);
                if (!len(tg.closest($doc))) return;// for change year dd, or rem click.ddp

                if (!isChildPopup(tg, pp.id)) {
                    if (!tg.closest($opener).length) {
                        api.close({ nofocus: 1 });
                    }
                }
            } else {
                //$doc.off(sddpOutClEv, outClickClose);
                unbind(o, outClickClose);
            }
        }

        function loadHandler() {
            layPopup();
        }

        $dropdownPopup.on(saweload + ' ' + sawebeginload, loadHandler);

        function resizeHandler() {
            layPopup(1, 1);
        }

        bind(o, $win, 'resize awedomlay', resizeHandler);
        //$win.on('resize awedomlay', resizeHandler);

        api.lay = resizeHandler;

        api.open = function (opt) {
            popt = opt || {};
            var e = popt.e;            
            if (toggle) {
                if (cx.isOpen) {
                    return api.close();
                }
            }

            sopener = popt.opener || sopener;

            if (sopener) {
                $opener = $(sopener);
            } else {
                if (e && e.target) {
                    $opener = trg(e);
                    var btn = $opener.closest('button');
                    if (btn.length) $opener = btn;
                }

                if (o.f && o.f.closest('.awe-field').length) {
                    $opener = o.f;
                }

                if ($opener && !$opener.is(':visible')) {//|| p.f
                    $opener = null;
                }
            }

            isFixed = 1;

            if ($opener) {
                openerId = $opener.attr('id');
                var parPop = $opener.closest('.o-pmc');

                if (parPop.length) {
                    zIndex = parPop.css(szindex);
                    if (parPop.css(sposition) !== 'fixed') {
                        isFixed = 0;
                    }
                } else {
                    isFixed = isPosFixed($opener);
                    zIndex = calcZIndex(zIndex, $opener);
                }
            }

            if (!isDropDown) {
                isFixed = 1;
                header.show();
            } else {
                itmoved = 0;
            }

            if (showHeader) {
                header.show();
            } else {
                header.hide();
            }

            if (!o.pwrp) {
                o.modal && host.append(modal);
            }

            host.append(wrap);
            wrap.show();
            cx.isOpen = 1;

            layPopup(0, isDropDown);

            kvIdOpener[pp.id] = $opener;

            setTimeout(function () {
                if (!cx.isOpen) return;
                bind(o, $doc, sddpOutClEv, outClickClose);
            }, 100);

            if (!isMobile() && !pp.nf) {
                setTimeout(function () {
                    var popTab = tabbable(popup).first();
                    if (popTab.length) {
                        popTab.focus();
                    } else {
                        tabbable(wrap).first().focus();
                    }
                },
                    10);
            }

            popup.trigger('aweopen');
        };

        api.close = function (opt) {            
            opt = opt || {};            

            cx.isOpen = 0;

            var nofocus = opt.nofocus || o.noCloseFocus;

            popup.trigger('awebfclose', opt);
            if (opt.noclose && !opt.force) return; // noclose can be set by awebfclose handler

            itmoved = 0;
            wrap.hide();
            if (modal) modal.hide();

            if (pp.close) {
                pp.close();
            }

            popup.trigger('aweclose', { out: nofocus });

            unbind(o, outClickClose);

            if (!(pp.loadOnce || pp.pwrp)) {

                if (pp.div) {
                    wrap.after(popup);
                }

                awe.destroyCont(wrap);
                wrap.remove();

                if (modal) modal.remove();
            }

            if (!pp.loadOnce) {
                awe.destroy(o);
            }

            if (!nofocus) {
                if (o.fcs) {
                    o.fcs.focus();
                }
                else if ($opener && len($opener)) {

                    $opener.focus();
                    var tofoc = $opener.find('.o-tofoc');
                    if (len(tofoc)) {
                        tofoc.focus();
                    }
                    else {
                        awe.tabbable($opener).first().focus();
                    }
                }
            }

            return true;
        };

        api.destroy = function () {
            awe.destroyCont(wrap);
            api.close({ force: 1, nofocus: 1 });
            !pp.pwrp && wrap.remove();
            if (modal) modal.remove();
            $win.off('resize awedomlay', resizeHandler);
            awe.destroy(o);
        };

        popup.data('api', api);
        header.find('.o-cls').click(api.close);

        function getDragPopup() {
            itmoved = 1;
            return $dropdownPopup;
        }

        if (!isDropDown && popupDraggable && pp.draggable != false) {
            dragAndDrop({
                from: header,
                ch: getDragPopup,
                kdh: 1,
                cancel: function () { return fls; }
            });
        }

        addFooter(btns, $dropdownPopup, popup, 'o-pbtns');

        function closeOnEsc(e) {
            if (which(e) === keyEsc) {

                if (!popup.data('esc')) {
                    api.close({ esc: 1 });
                }

                popup.data('esc', null);
            }
        }

        $dropdownPopup.on(skeyup, closeOnEsc);

        return wrap;
    }

    function addFooter(btns, cont, popup, fclass) {
        // add btns if any
        if (len(btns)) {
            var footer = $('<div/>').addClass(fclass);

            loop(btns, function (el) {
                var cls = !el.ok ? 'awe-sbtn' : 'awe-okbtn';
                var btn = $(rbtn('awe-btn ' + cls + ' o-pbtn', el.text));

                if (el.tag) {
                    var tag = el.tag;
                    if (tag.K)
                        loop(tag.K, function (key, indx) {
                            btn.attr(key, tag.V[indx]);
                        });
                }

                btn.click(function () { el.action.call(popup); });
                footer.append(btn);
            });

            cont.append(footer);
        }
    }

    function readTag(o, prop, nullVal, opt) {
        var res = nullVal;
        var tag = o.tag || o.Tag;

        if (tag) {
            if (isNotNull(tag[prop])) {
                res = tag[prop];
            } else {
                var lname = toLowerFirst(prop);
                if (isNotNull(tag[lname])) {
                    res = tag[lname];
                }
                else if (opt && isNotNull(opt[lname])) {
                    return opt[lname];
                }
            }
        }

        return res;
    }

    function layDropdownPopup2(o, pop, isFixed, capHeight, opener, setHeight, keepPos, canShrink, chkfulls, minh, popuph, maxph, popup, popt, postop, minbotd) {
        var scrolltop = $win.scrollTop();
        var marg = o.marg || 0;

        var opTop, opOutHeight = 0, opLeft, opOutWidth = 0;
        popt = popt || {};
        var xy = popt.xy;
        var right = popt.right;

        if (xy) {
            opener = 1;
            opTop = xy.y + scrolltop;
            opLeft = xy.x + $win.scrollLeft();
        } else if (opener) {
            opTop = opener.offset().top;
            opOutHeight = outerh(opener);
            opLeft = opener.offset().left;
            opOutWidth = outerw(opener);
        }

        if (!keepPos) {
            pop.css('left', 0);
            pop.css('top', 0);
        }

        var winh = $win.height();
        var winw = $win.width();

        var maxw = popt.maxw || winw - popSpace;
        var mnw = Math.min(outerw(pop), maxw);

        pop.css('min-height', se);
        pop.css(sheight, se);
        pop.css('max-width', maxw);

        pop.css(sminw, canShrink ? se : mnw);

        pop.css(sposition, se);

        var toppos;
        var left;

        var topd = scrolltop;
        var topCapHeight;

        if (opener) {
            topd = opTop;
            capHeight = capHeight || opOutHeight;

            topCapHeight = capHeight;

            if (right) {
                capHeight = 0;
            }
        }

        // handle opener overflow
        var botoverflow = topd - (winh + scrolltop);
        if (botoverflow > 0) {
            topd -= botoverflow;
        }

        var topoverflow = scrolltop - (topd + topCapHeight);

        if (topoverflow > 0) {
            topd += topoverflow;
        }

        var top = topd - scrolltop;
        var bot = winh - (top + capHeight);

        // adjust height
        var poph = popuph || outerh(pop);

        if (!o.maxph) o.maxph = poph;
        else if (o.maxph > poph) poph = o.maxph;
        else o.maxph = poph;

        var autofls = chkfulls(poph);

        var valign = 'bot';
        var fullsclass = 'o-fls';
        if (autofls) {
            isFixed = 1;
            addClass(pop, fullsclass);
        } else {
            removeClass(pop, fullsclass);
            var maxh = 0;
            if (opener) {
                var stop = top - hpSpace;
                var sbot = bot - hpSpace;
                maxh = sbot;

                if (minh > poph) minh = poph;

                if (sbot > poph || minbotd && sbot > minbotd) {
                    valign = 'bot';
                } else if (stop > sbot) {
                    valign = 'top';
                    if (poph > stop) {
                        poph = stop;
                    }

                    maxh = stop;
                } else {
                    poph = sbot;
                }

                if (poph < minh) {
                    maxh = poph = minh;
                }

                if (maxph && poph > maxph) {
                    poph = maxph;
                }

                if (poph > winh - popSpace) {
                    maxh = poph = winh - popSpace;
                }
            } else {
                maxh = winh - popSpace;
                if (postop) {
                    maxh -= popTopSpace;
                }
            }

            setHeight(poph, maxh, valign);
        }

        if (popup) {
            popup.trigger('aweresize');
        }

        if (isFixed) {
            topd = top;
            pop.css(sposition, 'fixed');
        }

        var w = outerw(pop);
        var h = outerh(pop);
        if (o.avh) h = o.avh + o.nph;


        if (opener) {
            left = opLeft + (right ? opOutWidth : 0);
            var lspace = winw - (left + w);
            if (lspace < 0) {
                if (right) {
                    if (w < opLeft) {
                        left = opLeft - w;
                    }
                }
                else if (opOutWidth < w) {
                    left -= w - opOutWidth;
                }

                if (left + w > winw) {
                    left -= left + w - winw;
                }

                if (left < 10) {
                    left = 10;
                }
            }

            if (autofls) {
                left = toppos = hpSpace;
            } else if (bot < h + hpSpace && (top > h + hpSpace || top > bot)) {
                // top
                toppos = topd - h - marg;
                if (right) toppos += topCapHeight;

                if (top < h) {
                    toppos = topd - top;
                    if (h + hpSpace < winh) toppos += hpSpace;
                }
            } else {
                // bot
                toppos = topd + capHeight + marg;

                if (bot < h + hpSpace) {

                    toppos -= h - bot;

                    if (h + hpSpace < winh) toppos -= hpSpace;
                }
            }
        } else {
            // no opener, center popup
            if (postop) {
                toppos = hpSpace + popTopSpace;
            } else {
                var diff = winh - h;
                toppos = diff / 2;
                if (diff > 200) toppos -= 45;
            }

            left = Math.max((winw - outerw(pop)) / 2, 0);
        }

        if (!keepPos || autofls) {
            pop.css('left', left);
            pop.css('top', Math.floor(toppos));
        }
    }
    //#endregion awem sync

    function serializeAllForm(frm) {
        return frm.find(':input').serializeArray();
    }

    function serializaInpAsObj(inp) {
        var res = {};
        inp.each(function () {
            var el = $(this);
            if (!el.attr('name') || el.hasClass('o-itmv')) return true;

            var v = el.val();
            if (isNotNull(v) && v !== se) {
                res[el.attr('name')] = el.val();
            }
        });

        return res;
    }

    function areScritEq(a, b) {
        return JSON.stringify(a) === JSON.stringify(b);
    }

    function strEquals(x, y) {
        if (isNull(x) || isNull(y)) {
            return x === y;
        }

        return x.toString() === y.toString();
    }

    function emptyFunc() { }

    function cd() {
        return awem.clientDict;
    }

    function kp(item) {
        return item.k;
    }

    function cp(item) {
        return item.c;
    }

    function isTrgIt(e, it) {
        return trg(e).is(it);
    }

    function econ(item, o) {
        var content = cp(item);
        return o.nenc ? content : encode(content);
    }

    function format(s, args) {
        return s.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] !== 'undefined'
                ? args[number]
                : match;
        });
    }

    function ensureArray(arr) {
        if (arr && !(arr instanceof Array)) {
            arr = [arr];
        }

        return arr;
    }

    function slowAct() {
        var tm;
        return function (func, time) {
            clearTimeout(tm);
            tm = setTimeout(func, time);
        }
    }

    function rbtn(cls, cont, attr) {
        if (isNull(attr)) attr = se;
        return '<button type="button" class="' + cls + '" ' + attr + '>' + cont + '</button>';
    }

    function awed(o) {
        return o.id ? ' id="' + o.id + '-awed" ' : se;
    }

    function rdiv(cls, cont, attr) {
        if (isNull(cont)) cont = se;
        if (isNull(attr)) attr = se;
        return '<div class="' + cls + '" ' + attr + '>' + cont + '</div>';
    }

    function toUpperFirst(s) {
        return s.substring(0, 1).toUpperCase() + s.substring(1);
    }

    function toLowerFirst(s) {
        return s.substring(0, 1).toLowerCase() + s.substring(1);
    }

    function containsVal(itemK, vals) {
        for (var i = 0; i < vals.length; i++) {
            if (strEquals(itemK, vals[i])) {
                return 1;
            }
        }

        return 0;
    }

    function gu(opt) {
        var model = opt.model;

        function getVal(prop) {
            var val = isNull(prop) ? se : guc(opt.o, prop, model);

            if (isNull(val)) val = se;
            return val instanceof Array ? JSON.stringify(val) : val;
        }

        return {
            parse: function (str, value) {
                str = str || '';
                var boolVal = value ? 'checked' : se;
                str = str.split('#Value').join(encode(value))
                    .split('#Prefix').join(opt.prefix)
                    .split('#ValChecked').join(boolVal);

                for (var key in model) {
                    var sval = encode(getVal(key));
                    str = str.split(".(" + key + ")").join(sval)
                        .split(".(" + toUpperFirst(key) + ")").join(sval);
                }

                str = str.replace(/\.\(\w+\)/g, "");
                return str;
            },
            getVal: getVal
        };
    };

    function getIxInArray(val, vals) {
        var res = -1;
        val = toStr(val);
        for (var i = 0; i < vals.length; i++) {
            if (val === vals[i]) {
                res = i;
                break;
            }
        }

        return res;
    }

    function loopTrees(items, func1) {
        for (var i = 0; i < items.length; i++) {
            var item = items[i];

            if (func1(item)) {
                return 1;
            }

            if (item.it) {
                if (loopTrees(item.it, func1)) return 1;
            }
        }
    }

    function addDist(arr, val) {
        if (arr.indexOf(val) < 0) {
            arr.push(val);
        }
    }

    function where(list, func) {
        var res = [];
        loop(list,
            function (el) {
                if (func(el)) {
                    res.push(el);
                }
            });

        return res;
    }

    function loopTreesRoot(items, func1, root) {
        for (var i = 0; i < items.length; i++) {
            var item = items[i];

            if (func1(item, root)) {
                return 1;
            }

            if (item.it) {
                if (loopTreesRoot(item.it, func1, root || item)) return 1;
            }
        }
    }

    function strContainsi(c, squeryUp) {
        return (c || se).toString().toUpperCase().indexOf(squeryUp) !== -1;
    }

    function strConainsLocale(c, squeryUp) {
        var con = (c || se).toString().toUpperCase()

        if (len(squeryUp) > len(con)) return false;

        function normletters(s) {
            return s.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase();
        }

        return normletters(con).includes(normletters(squeryUp));
    }

    function strStartsi(c, squeryUp) {
        return (c || se).toString().toUpperCase().substring(0, squeryUp.length) === squeryUp;
    }

    function strEqualsi(c, squeryUp) {
        return (c || se).toString().toUpperCase() === squeryUp;
    }

    function pickAvEl(arr) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].length) {
                return arr[i];
            }
        }
    }

    function setDisabled(o, s) {
        if (s) {
            o.attr(sdisabled, sdisabled);
        } else {
            o.removeAttr(sdisabled);
        }
    }

    function prevDef(e) {
        e.preventDefault();
    }

    var entityMap = {
        "<": "&lt;",
        '"': '&quot;',
        "'": "&#39;",
        ">": "&gt;",
        "&": "&amp;"
    };

    function toStr(v) {
        return isNull(v) ? se : v.toString();
    }

    function unesc(str) {
        str = toStr(str);
        for (var key in entityMap) {
            if (entityMap.hasOwnProperty(key)) {
                str = str.split(entityMap[key]).join(key);
            }
        }
        return str;
    }

    function dapi(o) {
        return o.data('api');
    }

    function dto(o) {
        return o.data('o');
    }

    function datesDif(d1, d2) {
        return !dateEq(d1, d2);
    }

    function dateEq(d1, d2) {
        return d1.getTime() === d2.getTime();
    }

    function dateOnlyEq(d1, d2) {
        var dA = new Date(d1);
        var dB = new Date(d2);
        dA.setHours(0, 0, 0, 0);
        dB.setHours(0, 0, 0, 0);

        return dA.getTime() == dB.getTime();
    }

    function setGridHeight(grid, newh) {
        var go = dto(grid);
        if (go && grid.is(':visible') && go.h !== newh) {
            go.h = newh;
            dapi(grid).lay();
        }
    }

    function scrollTo(focused, cont) {
        function y(o) {
            return o.offset().top;
        }

        var fry = y(focused);
        var fh = focused.height();
        var conh = cont.height();
        var miny = y(cont);
        var maxy = miny + conh - fh;
        var scrcont = cont;
        var winmax = $win.height() + $doc.scrollTop() - fh;
        var winmin = $doc.scrollTop();

        if (maxy > winmax && winmax < fry) {
            maxy = winmax;
            scrcont = $win;
        }

        if (miny < winmin && winmin > fry) {
            miny = winmin;
            scrcont = $win;
        }

        var delta = fry < miny ? fry - miny : fry > maxy ? fry - maxy : 0;

        // +1 for ie and ff 
        if (delta > fh + 1 && scrcont !== $win) {
            delta += conh / 2;
        }

        scrollTopBy(scrcont, delta);
    }

    function disbAttr(o) {
        return o.enb ? se : ' disabled="disabled"';
    }

    function cdelta(grid, val) {
        grid.trigger(sawerowch, val);
    }

    function movedGridRow(fromgrid, togrid) {
        dto(togrid).lrso = 1;
        dto(fromgrid).lrso = 1;
        cdelta(togrid, 1);
        cdelta(fromgrid, -1);
        if (!fromgrid.find(sawerowc).length && dto(fromgrid).lrs.pc > 1) {
            dapi(fromgrid).load();
        }
    }

    function year(date) {
        return date.getFullYear();
    }

    function hour24ToTt(d) {
        var val = d.getHours();
        if (val == 0) return 12;
        if (val > 12) return val - 12;
        return val;
    }

    var dateElmFrmFunc = {
        'mm': function (d) {
            return addZero(d.getMinutes());
        },
        'HH': function (d) {
            return addZero(d.getHours());
        },
        'h': function (d) {
            return hour24ToTt(d);
        },
        'd': function (d) {
            return d.getDate();
        },
        'dd': function (d) {
            return addZero(d.getDate());
        },
        'M': function (d) {
            return d.getMonth() + 1;
        },
        'MM': function (d) {
            return addZero(d.getMonth() + 1);
        },
        'yyyy': function (d) {
            return year(d);
        },
        'yy': function (d) {
            return year(d).toString().substring(2);
        },
        'tt': function (d) {
            return d.getHours() >= 12 ? 'PM' : 'AM';
        }
    };

    function dateElmParseFunc(prop, maxlen, reqlen, customf) {
        return function (s, res, baseDate) {
            s = s.trim();
            if (baseDate) {
                if (len(s) > maxlen) {
                    s = s.substring(0, maxlen);
                }

                if (!len(s) || reqlen && len(s) !== reqlen) {
                    return;
                }
            }

            if (s.indexOf('.') > -1) res.c = 1;

            var nval = customf ? customf(s) : Number(s);

            res[prop] = nval;
        };
    }

    function yf(n) {
        n = Number(n);
        if (n > 99) return null;
        if (n > 50) n += 1900;
        else n += 2000;
        return n;
    }

    var parseDtElmFrm = {
        // prop to add to the date parse res obj
        'd': dateElmParseFunc('d', 2),
        'dd': dateElmParseFunc('d', 2),
        'm': dateElmParseFunc('min', 2),
        'mm': dateElmParseFunc('min', 2),
        'yyyy': dateElmParseFunc('y', 4, 4),
        'yy': dateElmParseFunc('y', 2, 0, yf),
        'HH': dateElmParseFunc('hour', 2),
        'h': dateElmParseFunc('hour', 2),
        'MM': dateElmParseFunc('month', 2),
        'M': dateElmParseFunc('month', 2),
        'tt': dateElmParseFunc('tt', 2, 2, function (s) { return s; }),
    };

    function parseDate(frmt, value, baseDate, mind, maxd, timeFromValOnly) {
        var initFormat = frmt;
        if (!value) return baseDate;
        function getHead(s) {
            var l = 4;
            while (l) {
                var hd = s.substring(0, l);
                if (dateElmFrmFunc[hd]) {
                    return hd;
                }
                l--;
            }
        }

        var mis = 0;
        var res = {};
        while (frmt) {
            var dtElm = getHead(frmt);

            if (dtElm) {
                frmt = frmt.substring(dtElm.length);
                var str;

                if (frmt) {
                    // get the str to convert
                    var nextHead = getHead(frmt);
                    var sepix;
                    if (!nextHead) {
                        sepix = value.indexOf(frmt[0]);
                    } else {
                        sepix = 2;
                        if (dtElm === 'yy')
                            sepix = 4;
                    }

                    if (sepix === -1) {
                        mis = 1;
                        break;
                    }

                    str = value.substring(0, sepix);
                    value = value.substring(sepix + 1);
                }
                else {
                    str = value;
                }

                if (str === se) {
                    mis = 1;
                    break;
                }

                parseDtElmFrm[dtElm](str, res, baseDate);

            } else {
                frmt = frmt.substring(1);
            }
        }

        if (mis || res.c) {
            return 0;
        }

        if (timeFromValOnly) {
            if (isNull(res.hour) || isNull(res.min) || getIsAmPm(initFormat) && isNull(res.tt)) {
                return null;
            }
        }

        var month = res.month;

        if (baseDate) {
            if (isNull(res.y)) res.y = year(baseDate);
            if (isNull(month) || month > 12 || month < 1) month = baseDate.getMonth() + 1;
            var maxDate = new Date(res.y, month, 0).getDate();
            if (isNull(res.d) || res.d < 1 || res.d > maxDate) res.d = baseDate.getDate();
        }        

        var newDate = new Date(res.y, month - 1, res.d);

        if (isNotNull(res.hour)) {
            var hour = res.hour;
            var tt = (res.tt || '').toUpperCase();

            hour = hourTtTo24(hour, tt);

            newDate.setHours(hour, res.min);
        }

        if (!baseDate) {
            if (year(newDate) !== res.y || newDate.getMonth() !== month - 1) return 0;
        }

        if (mind && newDate < mind) {
            newDate = cdate(mind);
        }
        else if (maxd && newDate > maxd) {
            newDate = cdate(maxd);
        }
        
        return newDate;
    }

    function hourTtTo24(hour, tt) {
        if (tt == 'AM' && hour == 12) hour = 0;
        else if (tt == 'PM' && hour != 12) hour += 12;
        return hour;
    }

    function cdate(d) {
        return d ? new Date(d) : new Date();
    }

    function lastDayOfMonth(d) {
        var nd = cdate(d);
        nd.setMonth(d.getMonth() + 1);
        nd.setDate(0);
        return nd;
    }

    function formatDate(dtFrm, date) {
        var res = '';
        if (!date) return res;
        while (dtFrm) {
            var i = 4;
            while (i) {
                var dtElm = dtFrm.substring(0, i);
                var pfunc = dateElmFrmFunc[dtElm];
                if (pfunc) {
                    res += pfunc(date);
                    dtFrm = dtFrm.substring(dtElm.length);
                    break;
                }
                i--;
            }

            if (!i) {
                res += dtFrm.substring(0, 1);
                dtFrm = dtFrm.substring(1);
            }
        }

        return res;
    }

    function addZero(val) {
        if (val.toString().length < 2) {
            val = '0' + val;
        }

        return val;
    }

    function getItmByLiUpdateNode(items, li, newVal) {
        function gitem(ixs, itms) {
            if (ixs.length === 1) {
                if (newVal) {
                    itms[ixs[0]] = newVal;
                }

                return itms[ixs[0]];
            }

            var i = ixs.shift();
            itms[i].cl = 0; // expand node
            return gitem(ixs, itms[i].it);
        }

        var ix = li.data('index');
        var inxs;
        if (ix.split) {
            inxs = ix.split(',');
        } else {
            inxs = [ix];
        }

        return gitem(inxs, items);
    }

    function buttonGroupRadio(o) {
        return nbuttonGroup(o);
    }

    function buttonGroupCheckbox(o) {
        return nbuttonGroup(o, 1);
    }

    function nbuttonGroup(o, multiple) {
        var $odisplay;

        function init() {
            $odisplay = o.mo.odisplay;
            o.f.addClass('o-btng');

            $odisplay.on(sclick, '.awe-btn', function () {
                o.api.toggleVal($(this).data('val'));
            });
        }

        function setSelectionDisplay() {
            var val = awe.val(o.v);

            var items = se;
            loop(o.lrs, function (item, index) {
                var selected = containsVal(kp(item), val) ? saweselected : "";
                items += rbtn('awe-btn awe-il ' + selected, econ(item, o), 'data-index="' + index + '" data-val="' + encode(kp(item)) + '"' + disbAttr(o));
            });

            $odisplay.html(items);
        }

        function setSelectionDisplayChange() {
            var vals = awe.val(o.v);
            $odisplay.children(saweselectedc).removeClass(saweselected);
            loop(vals, function (v) {
                $odisplay.children().filter(function () {
                    return strEquals($(this).data('val'), v);
                }).addClass(saweselected);
            });
        }

        var opt = {
            setSel: setSelectionDisplay,
            setSelChange: setSelectionDisplayChange,
            init: init,
            multiple: multiple,
            noMenu: 1,
            ncmp: 1
        };

        return odropdown(o, opt);
    }

    function multiselb(o) {
        o.d.addClass("multiselb");
        function renderCaption() {
            return o.mo.inlabel;
        }

        return odropdown(o, {
            multiple: 1,
            renderCaption: renderCaption
        });
    }

    function multichk(o) {
        o.f.addClass("o-multichk");
        var opt = {
            multiple: 1,
            renderCaption: renderCaption,
            noSelClose: true
        };

        function renderCaption(sel) {
            var caption = o.mo.caption;
            var len = sel.length;
            if (len) {
                caption = opt.renSelCap(sel[0]);
            }

            if (len > 1) {
                caption = len + ' ' + cd().selected;
            }

            return o.mo.inlabel + caption;
        }

        return odropdown(o, opt);
    }

    function multiselect(o) {
        addClass(o.f, 'o-txtf');
        var cont = $(rdiv('o-mltic'));
        var srctxt = $('<input type="text" class="o-src awe-il awe-txt" ' + awed(o) + ' autocomplete="off" ' + disbAttr(o) + '/>');
        var caption = $('<span class="o-cptn"></span>');
        var glrs, api, comboPref, vprop, dmenu, dd;
        var isCombo = readTag(o, "Combo");
        var searchOutside = !isMobile() || isCombo;
        var ddopt = {};

        o.d.addClass("o-mltsl");
        var reor = readTag(o, "Reorderable");

        if (searchOutside)
            cont.append(srctxt);

        cont.prepend(caption);

        function init() {
            dd = o.mo.dd;
            dmenu = dd.cont;
            comboPref = o.mo.cp;
            vprop = o.mo.vprop;
            o.mo.odisplay.append(cont);
            caption.html(o.mo.caption);

            if (searchOutside) {
                o.mo.srctxt = srctxt;
            }

            api = o.api;
            glrs = api.glrs;
        }

        function renderSel(vals) {
            // add multiRem for vals
            var items = se;
            var lrs = glrs();
            loop(vals, function (val) {
                var found = 0;
                loopTrees(lrs, function (item) {
                    var itemv = item[vprop];
                    if (isNotNull(itemv)) itemv = itemv.toString();
                    if (itemv === val) {
                        items += renderSelectedItem(item);
                        found = 1;
                        return 1;
                    }
                });

                if (!found) {
                    var con = val;

                    if (isCombo && con.match('^' + comboPref)) {
                        con = con.replace(comboPref, se);
                    }

                    items += renderSelectedItem({ k: val, c: con });
                }
            });

            if (searchOutside) {
                srctxt.val(se);
                autoWidth(srctxt);

                srctxt.before(items);
            } else {
                cont.append(items);
            }

            renderCaption();
        }

        function renderCaption() {
            var istxtfocused = srctxt.is(':focus');

            var count = cont.find('.o-mlti').length;

            if (searchOutside) {
                srctxt.width(25);
            }

            if (count || istxtfocused) {
                caption.hide();
            } else {
                caption.show();
            }
        }

        var setSelectionDisplay = function () {
            cont.find('.o-mlti').remove();

            renderSel(awe.val(o.v));
        };

        function setSelectionDisplayChange() {

            var vals = awe.val(o.v);

            // remove keys
            cont.find('.o-mltrem').each(function () {
                var val = $(this).parent().data('val');
                var indexFound = getIxInArray(val, vals);
                if (indexFound > -1) {
                    // remove from vals
                    vals.splice(indexFound, 1);
                } else {
                    $(this).parent().remove();
                }
            });

            renderSel(vals);
        }

        function autoWidth(input) {
            input.css(swidth, Math.min(Math.max((input.val().length + 2) * 10, 25), cont.width()) + 'px');
        }

        function renderSelectedItem(item) {
            return rdiv('o-mlti awe-il awe-btn',
                '<span class="o-mltcptn">' + opt.renSelCap(item) + '</span><span class="o-mltrem" aria-label="remove">&times;</span>',
                'data-val="' + encode(item[vprop]) + '"');
        }

        function postSearchFunc(k) {
            if (!contains(k, nonComboSearchKeys) && searchOutside) {
                if (!dmenu.find(svalc + ':visible').length) {
                    dd.close();
                } else {
                    if (!(!srctxt.val() && k === keyBackspace)) {
                        dd.open();
                    }
                }
            }
        }

        function addComboVal(val) {
            var itemFound;
            var valu = val.toUpperCase();

            loopTrees(glrs(), function (item) {
                if (strEqualsi(cp(item), valu)) {
                    itemFound = item;
                    val = item[vprop];
                    return 1;
                }
            });

            if (!itemFound) val = comboPref + val;

            api.toggleVal(val, 1);
        }

        if (searchOutside) {
            srctxt.on(skeyup, function (e) {
                var k = which(e);
                var term = srctxt.val();
                if (!dd.isOpen() && !contains(k, nonOpenKeys)) {
                    if (!(k === keyBackspace && !term)) {
                        dd.open();
                    }
                }

                if (isCombo && k === keyEnter) {
                    if (term) {
                        addComboVal(term);
                        srctxt.val(se);
                    }
                }
            }).on(skeydown, function (e) {
                if (which(e) === keyBackspace && !srctxt.val()) {
                    cont.find('.o-mltrem:last').click();
                }

                if (which(e) === keyEnter) {
                    prevDef(e);
                }

                autoWidth(srctxt);
            }).on('focusin', function () {
                renderCaption();
                autoWidth($(this));
            }).on('focusout', function () {
                srctxt.val(se).change();
                renderCaption();
            });
        }

        cont.on(sclick, function (e) {
            if (!o.enb) return;
            if (!trg(e).is('.o-mltrem')) {
                dd.open();

                searchOutside
                    && !(isMobile() && trg(e).closest('.o-mlti').length)
                    && srctxt.width(1).focus(); // width 1 for focus on mobile 
                dd.lay();
            }
        });

        cont.on(sclick, '.o-mltrem', function (e) {
            if (!o.enb) return;
            var it = $(this);
            var val = it.parent().data('val');
            it.attr('awepid', o.id);
            api.toggleVal(unesc(val), null, 1);
            dd.close();
            searchOutside && srctxt.focus();
        });

        if (reor) {
            dragReor({
                from: cont,
                sel: '.o-mlti',
                tof: function () {
                    return [$('body')];
                },
                gcon: function () { return cont; },
                plh: 'awe-hl',
                cancel: function () { return !o.enb; },
                splh: 1,
                dropFunc: function (cx) {
                    var drgo = cx.drgo;
                    cx.plh.after(drgo).remove();
                    api.moveVal(drgo.data('val'), drgo.prev().data('val'));
                    drgo.show();
                }
            });
        }

        ddopt.afls = !isCombo;
        ddopt.psf = postSearchFunc;
        ddopt.noAutoSearch = searchOutside;
        ddopt.naa = 1;

        if (searchOutside) ddopt.srctxt = srctxt;

        var opt = {
            setSel: setSelectionDisplay,
            setSelChange: setSelectionDisplayChange,
            init: init,
            multiple: 1,
            prerender: function () { },
            combo: isCombo,
            d: ddopt,
            //ncmp: 1
        };

        return odropdown(o, opt);
    }

    function colorDropdown(o) {
        var caption;

        function init() {
            caption = o.mo.caption;
        }

        o.d.addClass("o-cldd");

        o.dataFunc = o.dataFunc || function () {
            return qsel(['#5484ED', '#A4BDFC', '#7AE7BF', '#51B749', '#FBD75B', '#FFB878', '#FF887C', '#DC2127', '#DBADFF', '#E1E1E1'],
                function (item) { return { k: item, c: item }; });
        };

        var renderCaption = function (selected) {
            var sel = caption;
            if (selected.length) {
                var color = kp(selected[0]);
                sel = '<div style="background:' + color + '" class="o-color">' + snbsp + '</div>';
            }

            return sel;
        };

        var renderItemDisplay = function (item) {
            return '<span class="o-clitm" style="background:' + kp(item) + '">' + snbsp + '</span>';
        };

        var opt = {
            renderCaption: renderCaption,
            init: init,
            menuClass: "o-clmenu",
            d: {
                renderItemDisplay: renderItemDisplay,
                noAutoSearch: 1
            }
        };

        odropdown(o, opt);
    }

    function genTimeEd(timeVal, id, isAmPm) {
        var res = lsted(id + 'hour', timeVal[0]);
        res += lsted(id + 'min', timeVal[1]);

        if (isAmPm) {
            res += lsted(id + 'tt', timeVal[2]);
        }

        return res;
    }

    function getTimeValArr(date, isAmPm) {
        if (!date) return [se, se, se];

        var hours = date.getHours();

        return [isAmPm ? hour24ToTt(date) : hours, date.getMinutes(), hours <= 12 ? 'AM' : 'PM'];
    }

    function getIsAmPm(format) {
        return format.indexOf('tt') != -1;
    }

    function runTmpJs(id, isAmPm) {
        var hourId = id + 'hour', minId = id + 'min', ttId = id + 'tt';
        awe.radioList({ id: hourId, nm: hourId, dataFunc: hourItems, md: awem.odropdown, tag: { Caption: '--'}, cmps: 1 });
        awe.radioList({ id: minId, nm: minId, dataFunc: minuteItems, md: awem.odropdown, tag: { Caption: '--' }, cmps: 1 });

        if (isAmPm) {
            awe.radioList({ id: ttId, nm: ttId, dataFunc: ttItems, md: awem.odropdown, tag: { Asmi: -1, Caption: '--' }, cmps: 1 });
        }

        function hourItems() {
            var range = [0, 23];

            if (isAmPm) {
                range = [1, 12];
            }

            var items = [];
            for (var i = range[0]; i <= range[1]; i++) {
                items.push({ k: i, c: addZero(i) });
            }

            return items;
        }

        function minuteItems() {
            var items = [];
            for (var i = 0; i <= 59; i++) {
                items.push({ k: i, c: addZero(i) });
            }

            return items;
        }

        function ttItems() {
            return [{ k: 'AM', c: 'AM' }, { k: 'PM', c: 'PM' }];
        }
    }

    function applyDispTime(ndate, timedds, isAmPm, fromValOnly) {
        var shour = timedds.hour.val();
        var smin = timedds.min.val();
        var stt = timedds.tt.val();

        if (fromValOnly && !(shour && smin && (stt || !isAmPm))) return;

        var nhour = Number(shour);
        var nmin = Number(smin);

        if (isAmPm) {
            nhour = hourTtTo24(nhour, stt)
        }

        ndate.setHours(nhour);
        ndate.setMinutes(nmin);
        return ndate;
    }

    function setTimeDds(timedds, id) {
        timedds.hour = $('#' + id + 'hour');
        timedds.min = $('#' + id + 'min');
        timedds.tt = $('#' + id + 'tt');
    }

    function setOnTimeChange(timedds, handler) {
        timedds.hour.change(handler);
        timedds.min.change(handler);
        timedds.tt.change(handler);
    }

    function tmp(o) {
        o.f.addClass("o-tmp");
        var timedds = {};
        var id = o.id;
        var format = o.format;
        var isAmPm = getIsAmPm(format);

        function getMainVal() {
            var sval = o.v.val();
            return sval ? parseDate(format, sval, new Date(), 0, 0, 1) : null;                
        }

        bind(o, o.v, schange, function () {            
            var vals = getTimeValArr(getMainVal(), isAmPm);

            function updateDd(elm, i) {
                elm.val(vals[i]);
                dapi(elm).render();
            }

            updateDd(timedds.hour, 0);
            updateDd(timedds.min, 1);
            isAmPm && updateDd(timedds.tt, 2);            
        });

        var date = getMainVal();
        var edhtml = genTimeEd(getTimeValArr(date, isAmPm), id, isAmPm);
        o.d.append(edhtml);

        if (o.clearBtn) {
            o.d.append(genClearBtn(o));
        }        

        addClass(o.d, 'o-cldtmp');
        setTimeDds(timedds, id);
        runTmpJs(id, isAmPm);

        awe.enable(o, o.enb != 0);

        setOnTimeChange(timedds, function () {
            var ndate = applyDispTime(new Date(), timedds, isAmPm, 1);
            var nsval = ndate ? formatDate(format, ndate) : se;
            var prev = o.v.val();
            if (prev != nsval) {
                o.v.val(nsval);
                awe.ach(o);
            }            
        });
    }

    function timepicker(o) {
        o.f.addClass("o-tmp");

        function pad(num) {
            var s = "00" + num;
            return s.substring(s.length - 2);
        }

        o.dataFunc = function () {
            var step = readTag(o, "Step") || 15;
            var items = [];
            var ampm = o.tag.AmPm;
            for (var i = 0; i < 24 * 60; i += step) {
                var apindx = 0;
                var hour = Math.floor(i / 60);
                var minute = i % 60;

                if (ampm) {

                    if (hour >= 12) {
                        apindx = 1;
                    }

                    if (!hour) {
                        hour = 12;
                    }

                    if (hour > 12) {
                        hour -= 12;
                    }
                }

                var item = ampm ? hour : pad(hour);

                item += ":" + pad(minute);

                if (ampm) item += " " + ampm[apindx];

                items.push(item);
            }

            return qsel(items, function (v) { return { k: v, c: v }; });
        };

        return combobox(o);
    }

    function combobox(o) {
        addClass(o.d, 'combobox');
        addClass(o.f, 'o-txtf');

        var $v = o.v;
        var disb = disbAttr(o);
        var cmbtxt = $('<input type="text" class="awe-txt o-cbxt o-src" size="1" autocomplete="off" ' + awed(o) + ' ' + disb + ' />');
        var $openbtn = $(rbtn('o-cbxbtn o-ddbtn awe-btn o-btn', scaret, 'aria-label="open" tabindex="-1"' + disb));
        var docClickReg = 0;
        var glrs;
        var clsEmptQuery = readTag(o, "ClsEq");
        var searchOnFocus = readTag(o, "Sof");
        var api, dd, dropmenu;
        var vprop;
        var contval = se;

        function init() {
            o.mo.odisplay.append(cmbtxt).append($openbtn);
            vprop = o.mo.vprop;
            cmbtxt.attr('placeholder', o.mo.caption);
            api = o.api;
            glrs = api.glrs;
            dd = o.mo.dd;
            dropmenu = dd.cont;
        }

        function setSelectionDisplay() {
            var vals = awe.val($v);

            var selected = [];

            function f1(item) {
                if (containsVal(item[vprop], vals) && !item.nv) {
                    selected = [item];
                    return 1;
                }
            }

            loopTrees(glrs(), f1);

            var txtval = se;
            if (!selected.length && vals.length) {
                txtval = vals[0];
            }
            else if (selected.length) {
                txtval = unesc(cp(selected[0]));
            }

            cmbtxt.val(txtval);
        }

        function onDocClick(e) {
            var tg = trg(e);

            if (!tg.closest(dropmenu).length &&
                !tg.closest(o.d).length &&
                tg.closest('[awepid]').attr('awepid') !== o.id) {

                // js click while focused won't loose txt focus

                compval();
                checkComboval();
                docClickReg = 0;

                if ($(':focus').closest(o.d).length) return;
                $doc.off('click focusin', onDocClick);
            }
        }

        cmbtxt.on('focusin', function () {
            this.selectionStart = this.selectionEnd = this.value.length;
            if (searchOnFocus) {
                setTimeout(function () { cmbtxt.select(); }, 10);
            }

            if (!docClickReg) {
                $doc.on('click focusin', onDocClick);
                docClickReg++;
            }
        }).on(skeydown, function (e) {
            if (which(e) === keyEnter && !dd.isOpen()) {
                prevDef(e);
                checkComboval();
            }
        }).on(skeyup, function (e) {
            var key = which(e);
            if (!dd.isOpen()) {
                if (contains(key, updownKeys)) {
                    dd.open();
                }

                if (key === keyEnter) {
                    checkComboval();
                }
            }
        });

        function postSearchFunc(k) {
            if (!contains(k, nonComboSearchKeys)) {
                if (!dropmenu.find(svalc + ':visible').length) {
                    dd.close();
                }

                compval();
            }
        }

        $openbtn.on(sclick, function () {
            dd.filter(se);
            if (!isMobile())
                cmbtxt.focus();
        });

        function compval() {
            var query = cmbtxt.val();
            var newVal = query;
            var cval = query;

            query = query.toUpperCase();

            loopTrees(glrs(), function (item) {
                if (strEqualsi(cp(item), query)) {
                    newVal = item[vprop];
                    cval = cp(item);
                    return 1;
                }
            });

            $v.data('comboval', newVal);
            contval = cval;
        }

        function checkComboval() {
            if (!$v.parent().length) {
                return;
            }

            var comboval = $v.data('comboval');

            if (isNotNull(comboval)) {
                if (o.v.val() !== comboval) {
                    api.toggleVal(comboval);
                    //cmbtxt.val(contval);
                }

                cmbtxt.val(contval);
            }
        }

        odropdown(o, {
            d: {
                noAutoSearch: 1,
                srctxt: cmbtxt,
                psf: postSearchFunc,
                afls: 0,
                ceq: clsEmptQuery,
                autosel: 1
            },
            setSel: setSelectionDisplay,
            setSelChange: setSelectionDisplay,
            combo: 1,
            init: init,
            prerender: function () { }
        });
    }

    function menuDropdown(o) {
        o.d.addClass("o-mdd");
        var opt = {
            menuClass: "o-mddm",
            noAutoSearch: 1,
            d: {
                click: function (zev) {
                    var $trg = $(zev.target);
                    if ($trg.is(svalc)) {
                        var click = $trg.data(sclick);

                        if (click) {
                            eval(click);
                        } else {
                            var $a = $trg.find('a');
                            if ($a.length)
                                $a.get(0).click();
                        }
                    }

                    o.mo.dd.close();
                },
                renderItemDisplay: function (item) {
                    var res;
                    var href = kp(item) || item.href;
                    if (href && !item.click) {
                        res = '<a href="' + href + '">' + econ(item, o) + '</a>';
                    } else {
                        res = econ(item, o);
                    }

                    return res;
                },

                renItAttr: function (i, it) {
                    var res = ' data-index="' + i + '" ';
                    if (it.click) res += ' data-click="' + it.click + '"';
                    return res;
                }
            }
        };

        return odropdown(o, opt);
    }

    function autocomplete(o) {
        var input = $('#' + o.id);
        var propId;
        var lastVal;
        var useCache = o.c;
        var dd = dropmenu({
            id: o.id, rtl: o.rtl, opener: input, srctxt: input, select: onSelect,
            sf: searchFunc, sfo: o, nacc: !useCache, minl: o.minl, dl: o.dl, itemFunc: o.itemFunc,
            psf: postSearchFunc, clsempt: 1, ck: o.ck, gval: gval, nom: 1, combo: 1, ceq: 1, nenc: o.nenc
        });

        addDestrFunc(o, dd.destroy);

        if (o.k) {
            propId = $('#' + o.k);
            input.on('keyup ' + schange, function () {
                if (input.val() !== lastVal) {
                    propId.val(se).change();
                }
            });
        }

        input.keyup(function (e) {
            var key = which(e);
            if (!dd.isOpen() && contains(key, updownKeys)) {
                dd.open();
            }
        }).on(saweload, dd.cc);

        function gval() {
            return input.val();
        }

        function onSelect(item) {
            var selval = unesc(item.c);
            input.val(selval).focus();

            if (propId) {
                propId.val(item.k).change();
                lastVal = selval;
            }
        }

        function postSearchFunc(k) {
            if (!contains(k, nonComboSearchKeys)) {
                if (!dd.cont.find(svalc + ':visible').length) {
                    dd.close();
                }
            }
        }

        function searchFunc(_, info) {
            var term = info.term;
            if (term.length < o.ml) return [];

            var c = info.cache;
            c.t = c.t || {}; // terms used
            c.n = c.n || []; // no result terms

            if (useCache) {
                if (c.t[term]) return [];
                c.t[term] = 1;

                // ignore terms that contain nr terms
                for (var i = 0; i < c.n.length; i++) {
                    if (term.indexOf(c.n[i]) >= 0) {
                        return [];
                    }
                }
            }

            var prm = awe.params(o);
            prm.push({ name: "v", value: term });

            var f = o.dataFunc ? o.dataFunc(term, prm) : awe.ajx({ url: o.url, data: prm, o: o });

            return $.when(f).done(function (data) {
                if (!data || !data.length) {
                    c.n.push(term);
                }

                return data;
            }).fail(function () {
                c.t[term] = 0;
            });
        }
    }

    function dataSource(glrs, cacheKey) {
        return {
            gc: function () {
                return cache[cacheKey];
            },
            cc: function () {
                cache[cacheKey] = null;
            },
            glr: function () {
                var lrs = glrs();

                var cacheObj = cache[cacheKey];
                if (cacheObj) {
                    var res = cacheObj.Items.slice(0);

                    loop(lrs.slice(0).reverse(), function (itm) {
                        if (isNull(cacheObj.Keys[kp(itm)])) {
                            res.unshift(itm);
                        }
                    });

                    return res;
                }

                return lrs;
            },
            addc: function (result, gval) {
                if (!cache[cacheKey]) cache[cacheKey] = { Items: [], Keys: {} };
                var cacheObj = cache[cacheKey];

                var keys = cacheObj.Keys;
                var items = cacheObj.Items;

                var chcount = 0;
                loopTrees(items, function () {
                    chcount++;
                });

                if (gval && chcount > cacheLimit) {
                    var vals = gval();
                    var nitems = [];
                    var nkeys = {};

                    if (vals.length) {
                        loopTreesRoot(items,
                            function (item, root) {
                                for (var j = 0; j < vals.length; j++) {
                                    if (strEquals(vals[j], item.k)) {
                                        nkeys[vals[j]] = nitems.length;
                                        nitems.push(root || item);
                                        vals.splice(j, 1);
                                        break;
                                    }
                                }
                            });
                    }

                    cache[cacheKey] = { Items: nitems, Keys: nkeys };
                    keys = nkeys;
                    items = nitems;
                }

                for (var i = 0; i < result.length; i++) {
                    var item = result[i];
                    var key = kp(item) || cp(item);
                    if (isNull(keys[key])) {
                        keys[key] = items.length;
                        items.push(item);
                    } else {
                        items[keys[key]] = item;
                    }
                }
            }
        };
    }

    // note: bundler (MVC5) refuses to rename params/vars in this function
    function dropmenu(dmo) {
        var srcThresh = autoSearchTresh;
        var id = dmo.i || dmo.id;
        var destroyOnClose;
        if (!id) {
            id = '__m' + identity++;
            destroyOnClose = 1;
        }

        //var filterFunc = strContainsi;
        var filterFunc = se.normalize ? strConainsLocale : strContainsi;
        if (dmo.flts) filterFunc = strStartsi;
        var getFunc = dmo.getFunc;
        var searchFunc = dmo.sf;
        var isCombo = dmo.combo;
        var comboPref = dmo.cmbp;
        var showCmbItm = dmo.cmbi;
        var opener, hpos, opBtn, olc;
        var itemFunc = eval(dmo.itemFunc);
        var noSelClose = dmo.noSelClose;
        var noAutoSearch = dmo.noAutoSearch;
        var vprop = dmo.vprop || 'k';
        var closeOnEmpty = dmo.clsempt;
        var maxh = dmo.maxh;
        var treeMenu = dmo.submenu;
        var submenu;
        var prnta = dmo.prnta, fprnt = dmo.fprnt;
        var nacc = dmo.nacc;
        var ds = dmo.ds || dataSource(dmo.dataFunc || function () { return []; }, dmo.ck || id);
        var getItems = ds.glr;

        var isFixed = 0;
        var zIndex = minZindex;
        var asmi = dmo.asmi;

        if (isNotNull(asmi)) {
            noAutoSearch = asmi === -1 ? 1 : 0;
            srcThresh = asmi;
        }

        var hostc = $('body');
        var srcinfo = '<li class="o-info">' + cd().SearchForRes + '</li>';
        var mainAttr = 'tabindex="-1" data-i="' + id + '"';
        var modal = $(rdiv('o-pmodal o-pu', se, mainAttr));

        var clss = 'o-menu o-pu ' + (dmo.clss || se);

        if (treeMenu) clss += ' o-tmn';

        var cont = $(rdiv(clss, se, mainAttr));
        var popt;

        var $itemscont = $(rdiv('o-itsc'));
        var mnits = $('<ul class="o-mnits" tabindex="-1">' + (searchFunc ? srcinfo : se) + '</ul>');

        var slistCtrl = slist($itemscont, { sel: svalc + ', .o-nod', afs: svalc + '.o-ditm', botf: botf, topf: topf });
        var autofocus = slistCtrl.autofocus;

        var $menuSearchCont = $(rdiv('o-srcc ' + soldngp, '<input type="text" class="o-src awe-txt" placeholder="' + cd().Search + '..." size="1"/>' + loadingHtml));
        var $menuSearchTxt = $menuSearchCont.find('.o-src');
        var srctxt = $menuSearchTxt;
        var outsrc;
        if (dmo.srctxt) {
            outsrc = 1;
            srctxt = dmo.srctxt;
        }

        var lastq;
        var isInit;

        if (isMobile())
            cont.addClass('o-mobl');

        if (dmo.rtl) {
            cont.addClass('awe-rtl').css('direction', 'rtl');
        }

        if (outsrc) {
            noAutoSearch = 1;
        }

        cont.append($menuSearchCont);
        cont.append($itemscont);
        $itemscont.append(mnits);
        $menuSearchCont.hide();

        dmo.renderItemDisplay = dmo.renderItemDisplay || function (item) {
            return itemFunc ? itemFunc(item) : econ(item, dmo);
        };

        dmo.renItAttr = dmo.renItAttr || function (i, it) {
            return 'data-index="' + i + '" data-val="' + it[vprop] + '"';
        };

        var getSrcTerm = dmo.gterm || function () {
            return srctxt.val().trim();
        };

        var onItemClick = dmo.click || function (e) {
            if (trg(e).closest('.awe-cbc').length) {
                return;
            }

            var clickedItem = $(this);

            var itm = null;
            if (!clickedItem.hasClass('o-cmbi')) {
                itm = getItmByLiUpdateNode(getItems(), clickedItem);
            }

            var cmbival = clickedItem.data('val');

            dmo.select && dmo.select(itm, cmbival);

            clickedItem.attr('awepid', id);
                        
            var keepFilterOnSelect = false;

            if (!noSelClose) {
                close();
                fprnt && fprnt.close();
            }

            if (!keepFilterOnSelect) {
                $menuSearchTxt.val(se);
            }

            if (noSelClose) {
                var term = se;
                if (keepFilterOnSelect) term = getSrcTerm();
                filter(term, clickedItem.data('index'));
                lay();
            }
        };

        //renderMenu();

        var dmid = id + '-dropmenu';
        $('#' + dmid).remove();
        $('#' + dmid + '-modal').remove();
        cont.attr('id', dmid);
        modal.attr('id', dmid + '-modal');

        setOpener(dmo);

        function mergeOpener(opt) {
            if (dmo.gopener) {
                opener = dmo.gopener();
                olc = hpos = null;
            }
            opener = opt.opener || opener;
            hpos = opt.hpos || hpos || opener;
            olc = opt.opnlc || olc || opener;

            if (!prnta) {
                opBtn = dmo.fcs || opener;
            }

            kvIdOpener[id] = hpos;
        }

        function setOpener(opt) {

            if (olc) {
                olc.off(skeydown, openerOnKeyDown);
            }

            mergeOpener(opt);
            var uidialog = 0, ddpop = 0;

            if (hpos) {
                uidialog = hpos.closest('.awe-uidialog');
                ddpop = hpos.closest('.o-pmc');
            }

            isFixed = 1;
            if (uidialog && uidialog.length) {
                hostc = uidialog;
                zIndex = hostc.css(szindex);
            } else if (hpos && hpos.parents('.modal-dialog').length) {
                hostc = hpos.closest('.modal');
                zIndex = hostc.css(szindex);
            } else if (ddpop && ddpop.length) {
                zIndex = ddpop.css(szindex);
                if (ddpop.css(sposition) !== 'fixed') {
                    isFixed = 0;
                }
            } else {
                isFixed = isPosFixed(hpos);
            }

            olc && olc.on(skeydown, openerOnKeyDown);

            if (!cont.closest(hostc).length) {
                hostc.append(modal.hide());
                hostc.append(cont);
            }
        }

        function openerOnKeyDown(e) {
            if (isOpen()) {
                handleMoveSelectKeys(e);
            }
        }

        function loadLazyNode(itm, li, noflt) {
            itm.l = 0;
            var ldng = $(loadingHtml);
            var addld = setTimeout(function () { li.prepend(ldng); }, 100);

            // lazy request
            return $.when(getFunc(kp(itm)))
                .done(function (res) {
                    clearTimeout(addld);
                    var lazyIt = res[0];
                    if (lazyIt) lazyIt._lz = 1;
                    getItmByLiUpdateNode(getItems(), li, lazyIt);
                    ldng.remove();
                    !noflt && filter(lastq, li.data('index'));
                });
        }

        function getActiveMenu(menu) {
            if (menu.submenu) return getActiveMenu(menu.submenu);
            return menu;
        }

        bind(dmo, $win, 'resize awedomlay', lay);

        if (!prnta && outsrc) {
            srctxt.on(skeyup, search);
        }

        function initCont() {
            if (isInit) return;
            isInit = 1;
            !outsrc && srctxt.on(skeyup, search);
            cont.on(sclick, svalc, onItemClick)
                .on(smousemove, svalc + ', .o-nod', function () {
                    slistCtrl.focus($(this));
                    dmo.prntRf && dmo.prntRf(); // refocus parent
                })
                .on(skeydown, handleMoveSelectKeys);

            cont.on(sclick, '.o-itm .awe-cbc', function () {
                var li = $(this).closest('.o-itm');
                var mylvl = parseInt(li.data('lvl'), 10);
                var next = li.next();
                var ditm = getItmByLiUpdateNode(getItems(), li);

                // expand
                if (li.hasClass(sawecollapsed)) {
                    // if lazy node
                    if (ditm.l) {
                        loadLazyNode(ditm, li);
                    } else {
                        ditm.cl = 0;
                        li.removeClass(sawecollapsed);
                        awe.cllp(next, mylvl, false);
                    }
                } // collapse
                else {
                    ditm.cl = 1;
                    li.addClass(sawecollapsed);
                    awe.cllp(next, mylvl, true);
                }
            });

            cont.on(skeydown, function (e) {
                if (which(e) === keyTab) {
                    prevDef(e);
                    opener && tabbable(opener).focus();
                }
            });

            if (treeMenu) {
                // reg open on hover
                var hopt = {
                    hover: mnits,
                    hsel: '.o-nod',
                    open: sbm,
                    close: function () {
                        submenu && submenu.destroy();
                    },
                    topen: 250,
                    tclose: 500,
                    shclose: function (e) {
                        return !isChildPopup(trg(e), submenu && submenu.id);
                    }
                };

                hovOpen(hopt);
            }
        }

        function destroy() {
            close();
            srctxt.off(skeyup, search);
            closeDestroy();
        }

        function closeDestroy() {
            cont.remove();
            modal.remove();
            unbind(dmo, lay);
            isInit = 0;
        }

        var api = {
            cc: clearc,
            filter: filter,
            open: open,
            topen: toggleOpen,
            close: close,
            isOpen: isOpen,
            cont: cont,
            render: renderMenu,
            destroy: destroy,
            lay: lay,
            slist: slistCtrl,
            submenu: submenu,
            id: id,
            sbm: sbm,
            focus: focus
        };

        cont.data('api', api);

        function sbm(mli, afcs) {
            var sbo = $.extend({}, dmo);
            var itm = getItmByLiUpdateNode(getItems(), mli);
            if (itm.l) {
                $.when(loadLazyNode(itm, mli, 1)).done(function () {
                    submenu.render();
                    submenu.lay();
                });
            }

            sbo.dataFunc = function () {
                return getItmByLiUpdateNode(getItems(), mli).it || [];
            };

            sbo.i = id + '_s';
            sbo.prnta = api;

            sbo.fprnt = fprnt || api;
            sbo.prntRf = function () {
                slistCtrl.focus(mli);
            };

            sbo.noaf = !afcs;
            sbo.sf = sbo.ds = sbo.opener = sbo.desf = null;
            sbo.srctxt = srctxt;

            var opix = mli.index();
            sbo.gopener = function () {
                return mnits.children().eq(opix);
            };

            submenu = dropmenu(sbo);
            submenu.open({ right: true });

            return submenu.cont;
        }

        function handleMoveSelectKeys(e) {
            // get active menu
            var menu = getActiveMenu(api);
            var mslist = menu.slist;
            mslist.keyh(e);
            var we = which(e);
            var noterm = !getSrcTerm();
            var focSbm = menu !== api;
            if (we === keyEsc || we === keyLeft && (prnta || focSbm && noterm)) {
                trg(e).closest('.awe-popup').data('esc', 1);
                menu.close();

                if (prnta) {
                    prnta.focus();
                }
                else if (!focSbm && opener) {
                    tabbable(opener).first().focus();
                }
            }

            if (noterm && (we === keyRight)) {
                var li = mslist.gfocus();
                if (li.is('.o-nod')) {
                    menu.sbm(li, 1);
                }
            }
        }

        var searchTimerOn;
        var searchTimerTerm;
        var searchTimer;
        var localSearchResCount;
        var itrkc = 0;

        function search(e) {
            if (!contains(which(e), nonComboSearchKeys) && !e.ctrlKey) {
                var term = getSrcTerm();

                // no search when term == -1
                if (term === -1) {
                    localSearchResCount = 0;
                    term = se;
                } else {
                    submenu && submenu.destroy();
                    localSearchResCount = filter(term);
                    if (localSearchResCount && !dmo.naa) {
                        open({ nor: 1 });
                    }
                }

                // close on empty query
                if (dmo.ceq && !term) {
                    close();
                }
                else if (closeOnEmpty && !localSearchResCount) {
                    close();
                }

                if (searchFunc && term) {
                    if (searchTimerOn) {
                        itrkc++;
                    }

                    if (!searchTimerOn) {
                        searchTimerOn = 1;
                        searchTimerTerm = term;

                        searchTimer = setInterval(function () {
                            var newTerm = getSrcTerm();

                            if (newTerm === searchTimerTerm && !itrkc) {
                                clearInterval(searchTimer);
                                searchTimerOn = 0;

                                if (searchTimerTerm) {
                                    srctxt.closest(soldngpc).addClass('o-ldng');

                                    var nsrcfunc = dmo.minl && searchTimerTerm.length < dmo.minl
                                        ? function () { return []; }
                                        : eval(searchFunc);

                                    ds.addc([]);
                                    $.when(nsrcfunc(dmo.sfo,
                                        {
                                            term: searchTimerTerm,
                                            count: localSearchResCount,
                                            cache: ds.gc()
                                        }))
                                        .always(function () {
                                            srctxt.closest(soldngpc).removeClass('o-ldng');
                                        })
                                        .done(function (result) {
                                            nacc && ds.cc();
                                            ds.addc(result, dmo.gval);

                                            renderMenu();

                                            if (getSrcTerm() !== -1) {
                                                var rescount = filter(getSrcTerm());

                                                if (!localSearchResCount && rescount) {
                                                    open({ nor: 1 }); // norender optimization
                                                }
                                            }

                                            lay();
                                            dmo.psf && dmo.psf(which(e));
                                        });
                                }
                            }

                            searchTimerTerm = newTerm;
                            itrkc = 0;

                        },
                            dmo.dl || 250);
                    }
                } else {
                    dmo.psf && dmo.psf(which(e));
                }
            }
        }

        function botf() {
            var st = $itemscont.scrollTop();
            var sth = $itemscont.height();
            var h = mnits.height();
            if (sth + st < h) {
                $itemscont.scrollTop(st + 25);
            }
        }

        function topf() {
            var st = $itemscont.scrollTop();
            if (st > 0) {
                $itemscont.scrollTop(st - 25);
            }
        }

        function renderMenu() {
            var rs = getItems();
            var count = renderList(rs);
            
            if ((count > srcThresh || searchFunc) && !noAutoSearch) {
                $menuSearchCont.show();
            } else {
                $menuSearchCont.hide();
            }
        }

        function renderList(rs, query) {
            var focusix = mnits.find('.focus').data('index');
            lastq = query;
            var unescf = function (s) { return s; };
            var itcount = 0;
            var matchFound = 0;
            var side = 'padding-' + (dmo.rtl ? 'right' : 'left') + ':';
            var res = se;
            var filtf = function () { return 1; };
            var encodedQuery = se;
            var txtvalup = srctxt.val().toUpperCase();
            var subm = treeMenu && !query;

            if (query) {
                encodedQuery = encode(query);
                var upquery = query.toUpperCase();
                if (query !== encodedQuery) {
                    unescf = unesc;
                }

                filtf = function (s) {
                    return filterFunc(s, upquery);
                };
            }

            var selVal;
            if (dmo.gval && !dmo.nom) {
                selVal = dmo.gval();
            }

            function addCount(items) {
                loop(items, function (itm) {
                    itcount++;
                    if (itm.it) {
                        addCount(itm.it);
                    }
                });
            }

            function renderNodes(items, ipref, lvl, hide, nodLzLoaded) {
                var nodesRes = se;
                loop(items, function (item, i) {
                    var c = unescf(cp(item));
                    var cls = item.cs || 'o-itm';
                    var selected = 0;
                    var itemCollapsed = item.cl;
                    var isLazy = item.l;
                    var isNode = item.it && item.it.length;

                    var lazyLoaded = item._lz;
                    item._lz = 0;

                    if (subm && (isNode || isLazy)) {
                        cls += ' o-nod';
                    }

                    var childRs = se;
                    if (isNode) {
                        if (subm) {
                            addCount(item.it);
                        } else {
                            childRs += renderNodes(item.it,
                                ipref + i + ',',
                                lvl + 1,
                                hide || itemCollapsed && !query,
                                lazyLoaded);
                        }
                    }

                    var ren = 0;

                    if (!query || childRs || isNode && subm || !item.nv && (nodLzLoaded || filtf(c))) {
                        ren = 1;
                    }

                    var attr = se;

                    if (ren) {
                        var clpBtn = (dmo.clp || subm) && (isNode || isLazy) ? "<i class='awe-cbc'><i class='" + (subm ? 'o-arw right' : 'awe-ce-ico') + "'></i></i>" : se;
                        if (itemCollapsed) cls += ' ' + sawecollapsed;
                        if (item.nv) {
                            cls += ' o-nv';
                        }
                        else {
                            cls += ' o-v';

                            if (query && isCombo && c.length === query.length) {
                                matchFound = 1;
                                selected = 1;
                            }

                            if (!dmo.nom
                                && selVal
                                && containsVal(item[vprop], selVal)
                                && (!dmo.autosel || strEqualsi(cp(item), txtvalup))) { // hide selVal when combo edit srctxt
                                selected = 1;
                            }
                        }

                        var style = se;
                        if (lvl) {
                            style = side + (lvl + 2) + 'em;';
                        }

                        if (selected) {
                            cls += ' ' + sselected;
                            if (dmo.rtl) {
                                style += 'background-position:right ' + (lvl || .5) + 'em center;';
                            }
                            else if (lvl) {
                                style += 'background-position-x:' + lvl + 'em;';
                            }
                        }

                        if (hide) {
                            style += 'display:none;';
                        }

                        if (style) {
                            attr = 'style="' + style + '" ';
                        }

                        attr += 'data-lvl="' + (lvl + 1) + '" ';

                        if (focusix === i) {
                            cls += ' ' + sfocus;
                        }

                        nodesRes = nodesRes + '<li class="' + cls + ' o-ditm" ' + attr + dmo.renItAttr(ipref + i, item) + '>'
                            + clpBtn + dmo.renderItemDisplay(item) + '</li>' + childRs;

                        itcount++;
                    }
                });

                return nodesRes;
            }

            res += renderNodes(rs, se, 0);

            if (isCombo && showCmbItm && query && !matchFound) {
                res = '<li class="o-itm o-v o-cmbi" data-val="' + comboPref + encodedQuery + '">'
                    + dmo.renderItemDisplay({ c: query, k: encodedQuery })
                    + '</li>' + res;
            }

            if (!len(rs)) {
                res += '<li class="o-empt">(' + cd().Empty + ')</li>';
            }

            if (searchFunc && !query) {
                res += srcinfo;
            }

            mnits.html(res);
            return itcount;
        }

        function docClickHandler(e) {
            if (istrg(e, '.o-pmodal') && e.type === 'touchstart') return;

            if (e.type === skeydown && (e.ctrlKey || which(e) === keyRight || istrg(e, srctxt))) return;

            if (istrg(e, '.awe-clrbtn') || !istrg(e, olc) && !isChildPopup(trg(e), id)) {
                close();
                opener && opener.trigger('aweblur');
                if (istrg(e, '.o-pmodal')) {
                    dmo.mdcls && dmo.mdcls();
                }
            }
        }

        function close() {
            if (isOpen()) toggleOpen();
        }

        function open(opt) {
            if (!isOpen()) toggleOpen(opt);
        }

        function isOpen() {
            return cont.hasClass('open');
        }
        function focus() {
            if (!(dmo.fprnt && dmo.fprnt.msrc) && !isMobile()) {
                if (srctxt.is(':visible')) {//!noAutoSearch || o.srctxt && 
                    api.msrc = 1;
                    srctxt.focus();
                } else {
                    mnits.attr('tabindex', '0').focus();
                }
            }
        }
        function toggleOpen(opt) {
            popt = opt;
            if (opt) {
                setOpener(opt);
            }

            opt = opt || {};
            cont.toggleClass('open');
            if (isOpen()) {
                dmo.maxph = 0;
                if (prnta) {
                    prnta.submenu = api;
                }

                if (hpos && zIndex) {
                    zIndex = calcZIndex(zIndex, hpos);
                }

                if (zIndex) {
                    modal.css(szindex, zIndex + 1);
                    cont.css(szindex, zIndex + 1);
                }

                bind(dmo, $doc, soddDocClEv, docClickHandler);

                // render to get h as if it was full
                if (!opt.nor) {
                    renderMenu(opt);
                }

                cont.show();

                lay();

                focus();

                !dmo.noaf && autofocus();

                if (opBtn) {
                    addClass(opBtn, sawefocus);
                    addClass(opBtn.closest('.awe-field'), sawefocus);
                }

                initCont();
            } else {
                if (opBtn) {                    
                    removeClass(opBtn, sawefocus);
                    if (!opBtn.is(':focus')) {                        
                        removeClass(opBtn.closest('.awe-field'), sawefocus);
                    }
                }

                cont.hide();
                modal.hide();

                unbind(dmo, docClickHandler);

                if ($menuSearchTxt.val() && lastq) {
                    $menuSearchTxt.val(se);

                    // reset search
                    filter(se);
                }

                if (prnta) {
                    prnta.submenu = null;
                }

                submenu && submenu.destroy();

                if (destroyOnClose) closeDestroy();
            }
        }

        function lay() {
            mergeOpener({});
            var maxpoph = maxh || maxDropdownHeight;
            if (maxh === 0) maxpoph = $win.height();

            if (!cont.hasClass('open')) return;

            var oitemsc = cont.find('.o-itsc');
            var oitemscst = oitemsc.scrollTop();

            cont.css(smaxh, maxpoph + 'px');
            cont.css(sheight, se);

            cont.css(swidth, se);

            var minw = dmo.minw || 0;
            var popt1 = popt || {};
            popt1.maxw = dmo.maxw;

            if (opener && !popt1.xy) {
                minw = Math.max(outerw(opener), minw);
            }

            if (minw) {
                cont.css(sminw, minw + 'px');
            }

            function chkfulls(height) {
                var winw = $win.width();
                var winh = $win.height();
                var limh = 300;
                var limw = 200;
                if (!isCombo && dmo.afls) {
                    if (height > winh - limh - popSpace && cont.width() > winw - limw - popSpace) {
                        cont.width(winw - popSpace);
                        setHeight(0, winh - popSpace - clickOutSpace, 'fls');

                        modal.show();
                        return 1;
                    } else {
                        modal.hide();
                    }
                }
            }

            function setHeight(poph, maxph, valign) {
                if (valign === 'top') {
                    cont.css(sheight, poph);
                } else {
                    var h = maxph;
                    if (valign !== 'fls') {
                        h = Math.min(h, maxpoph);
                    }

                    h && cont.css(smaxh, h);
                }
            }

            layDropdownPopup2(dmo, cont, isFixed, null, olc, setHeight, 0, 0, chkfulls, isCombo ? comboMinh : menuMinh, 0, maxpoph, 0, popt1, 0, 0);

            oitemsc.scrollTop(oitemscst);
        }

        function filter(query, index, noautofocus) {
            var items = getItems();

            var count = renderList(items, query);

            var focitem = index ? mnits.find('[data-index="' + index + '"]') : null;

            if (!noautofocus) {
                autofocus(focitem);
            }

            return count;
        }

        function clearc() {
            ds.cc();
            renderMenu();
        }

        return api;
    }

    function genClearBtn(o) {
        var btn = $(rbtn('awe-clrbtn awe-btn o-btn',
            '<span class="awe-icon awe-icon-x"></span>',
            disbAttr(o) + ' aria-label="clear value"'));

        btn.on(sclick, function () {
            o.v.val(se);
            awe.ach(o);
        })

        return btn;            
    }

    function odropdown(o, opt) {
        opt = opt || {};

        var inlabel = readTag(o, 'InLabel', se);
        var caption = readTag(o, 'Caption', cd().Select);
        var autoSelectFirst = readTag(o, 'AutoSelectFirst');
        var minWidth = readTag(o, 'MinWidth');
        var cacheKey = readTag(o, 'Key', o.id);
        var captionFunc = readTag(o, 'CaptionFunc');
        var useConVal = readTag(o, 'UseConVal');
        var comboPref = readTag(o, 'GenKey') ? '__combo:' : se;
        var openOnHover = readTag(o, "OpenOnHover");
        var noSelClose = readTag(o, 'NoSelClose', null, opt);
        var hasClrBtn = readTag(o, 'ClearBtn');
        var favCount = readTag(o, 'FavCount');
        var favs = readTag(o, 'Favs', []);
        var ffavs = readTag(o, 'FrozenFavs', []);
        favCount = Math.max(favs.length + ffavs.length, favCount);

        if (!opt.ncmp && !favCount) {
            addClass(o.f, 'o-cmped');
        }        

        if (favCount) {
            addClass(o.f, 'o-favs');
        }

        if (hasClrBtn) {
            o.f.addClass('o-hasclr');
        }

        var clearCacheOnLoad = readTag(o, 'ClearCacheOnLoad', 1);
        var bstr = rbtn('o-ddbtn o-btn awe-btn', scaret, awed(o) + disbAttr(o) + ' aria-label="open"');
        var btn = $(bstr);
        var clrBtn = genClearBtn(o);

        var $odropdown = $(rdiv('o-dd'));
        var $odisplay = $(rdiv('o-disp ' + soldngp, loadingHtml));
        var $valCont = $(rdiv('valCont')).hide();
        var btnCaption = $(rdiv('o-cptn', inlabel + caption));
        var vprop = useConVal ? 'c' : 'k';
        var valInputType = opt.multiple ? "checkbox" : "radio";
        var ds = dataSource(function () { return o.lrs; }, cacheKey);
        var glrs = ds.glr;
        var isCombo = opt.combo;

        var api = o.api;
        api.render = render;
        api.initev = initev;
        api.glrs = glrs;
        api.toggleVal = toggleVal;
        api.moveVal = moveVal;

        var fcs = opt.d ? null : btn;

        var dmcfg = {
            id: o.id,
            rtl: o.rtl,
            opener: $odropdown,
            ds: ds,
            sfo: o,
            select: onSelect,
            cmbp: comboPref,
            sf: readTag(o, 'SearchFunc'),
            submenu: readTag(o, 'Submenu'),
            nacc: readTag(o, 'NoCache'),
            getFunc: getFunc,
            cmbi: readTag(o, "CmbItm", 1),
            itemFunc: readTag(o, 'ItemFunc'),
            clss: readTag(o, "PopupClass", se) + ' ' + (opt.menuClass || se),
            //noSelClose: noSelClose,
            asmi: readTag(o, "Asmi"),
            combo: isCombo,
            vprop: vprop,
            fcs: fcs,
            opnlc: null,
            gval: function () { return awe.val(o.v); },
            afls: 1,
            mdcls: afterClose,
            maxh: readTag(o, 'PopupMaxHeight'),
            minw: readTag(o, 'PopupMinWidth'),
            maxw: readTag(o, 'PopupMaxWidth'),
            clp: readTag(o, 'CollapseNodes'),
            o: o,
            nenc: o.nenc
        };

        $.extend(dmcfg, opt.d);

        if (isNotNull(noSelClose)) {
            dmcfg.noSelClose = noSelClose;
        }

        noSelClose = dmcfg.noSelClose;

        if (minWidth) $odropdown.css(sminw, minWidth);

        $odropdown.append($odisplay);
        o.d.append($valCont).append($odropdown);
        o.f.addClass('o-field');

        var dd = dropmenu(dmcfg);
        api.dd = dd;

        addDestrFunc(o, dd.destroy);

        o.mo = { odisplay: $odisplay, caption: caption, odropdown: $odropdown, inlabel: inlabel, vprop: vprop, cp: comboPref, dd: dd };

        opt.renderCaption = opt.renderCaption || function (selected) {
            var sel = caption;
            if (selected.length) {
                sel = opt.renSelCap(selected[0]);
            }

            return inlabel + sel;
        };

        opt.renSelCap = opt.renSelCap || function (item) {
            return captionFunc ? eval(captionFunc)(item) : econ(item, o);
        };

        opt.setSel = opt.setSel || function () {
            btnCaption.html(getSelectedCaption());
            if (favCount) {
                renFavs(1);
            }
        };

        opt.setSelChange = opt.setSelChange || function () {
            btnCaption.html(getSelectedCaption());
            if (favCount) {
                renFavs();
            }
        };

        function renFavs(force) {
            var vals = awe.val(o.v);
            var vadded = 0;

            // add new vals
            loop(vals,
                function (v) {
                    if (!containsVal(v, favs) && !containsVal(v, ffavs) && kToItm(v)) {
                        favs.unshift(v);
                        vadded++;
                    }
                });

            if (!vadded && !force) {
                // part render
                var fvb = $odisplay.find('.o-favb');
                fvb.removeClass(saweselected);
                loop(ffavs.concat(favs), function (fv, j) {
                    if (containsVal(fv, vals)) {
                        fvb.eq(j).addClass(saweselected);
                    }
                });
                return;
            }

            var xfavs = [];
            loop(favs, function (v) {
                if (kToItm(v)) {
                    xfavs.push(v);
                }
            });
            favs = xfavs;

            var fcount = favCount - ffavs.length;
            if (favs.length > fcount) favs.length = fcount;

            loopTrees(glrs(), function (itm) {
                if (favs.length >= fcount) {
                    return 1;
                }

                var itemk = kp(itm);

                if (!itm.nv) {
                    addDist(favs, itemk);
                }
            });

            var fbtns = se;
            ren(kToItms(ffavs));
            ren(kToItms(favs));

            function ren(items) {
                for (var i = 0; i < items.length; i++) {
                    var item = items[i];

                    var clss = 'awe-btn o-favb o-btn ';
                    if (containsVal(kp(item), vals)) {
                        clss += saweselected;
                    }

                    fbtns += rbtn(clss, opt.renderCaption([item]), disbAttr(o));
                }
            }

            $odisplay.find('.o-favb').remove();
            $odisplay.prepend(fbtns);

            if (fbtns) {
                o.f.addClass('o-hasfav');
            } else {
                o.f.removeClass('o-hasfav');
            }
        }

        bind(o, $odropdown, sclick, '.o-favb', function (e) {
            toggleVal(unesc(ffavs.concat(favs)[trg(e).closest('.o-favb').index()]));
            dd.slist.autofocus();
        });

        opt.prerender = opt.prerender || function () {
            btn.prepend(btnCaption);

            $odisplay.append(btn);
        };

        function getFunc(k) {
            var lazyReq = o.lreq.slice(0);
            lazyReq.push({ name: "Key", value: k });
            return awe.gd(o, lazyReq);
        }

        function onSelect(item, val) {
            toggleVal(item ? unesc(item[vprop]) : val);
            !noSelClose && afterClose();
        }

        function afterClose() {
            var osrc = $odisplay.find('.o-src');
            if (osrc.length && !isMobile()) {
                osrc.focus();
            } else {
                if (opt.multiple) {
                    $odropdown.attr('tabindex', '0').focus().removeAttr('tabindex');
                } else {
                    btn.focus();
                }
            }
        }

        function kToItms(keys) {
            var res = [];

            loop(keys, function (key) {
                var itm = kToItm(key);
                if (itm) res.push(itm);
            });

            return res;
        }

        function kToItm(key) {
            var res;
            loopTrees(glrs(),
                function (item) {
                    if (!item.nv && strEquals(kp(item), key)) {
                        res = item;
                        return 1;
                    }
                });
            return res;
        }

        function getSelectedCaption() {
            var vals = awe.val(o.v);
            return opt.renderCaption(kToItms(vals));
        }

        function toggleVal(val) {
            if (opt.multiple) {
                var arrval = awe.val(o.v, 1);
                var exindx = getIxInArray(val, arrval);
                if (exindx > -1) {
                    // remove ex val
                    arrval.splice(exindx, 1);
                } else {
                    // add new val
                    arrval.push(toStr(val));
                }

                if (arrval && arrval.length) {
                    val = JSON.stringify(arrval);
                } else {
                    val = '';
                }
            }

            o.v.val(val);
            renderValInputs();
            awe.ach(o, { ign: 1 });
        }

        function moveVal(val, afval) {
            val = toStr(val);
            var arrval = awe.val(o.v);

            var from = getIxInArray(val, arrval);
            arrval.splice(from, 1);

            if (afval) {
                var to = getIxInArray(afval, arrval);
                arrval.splice(to + 1, 0, val);
            } else {
                arrval.unshift(val);
            }

            o.v.val(JSON.stringify(arrval));
            renderValInputs();
        }

        function initev() {
            function onChange(e, evdata) {
                if (!evdata || !evdata.ign) {
                    api.render();
                }
            }

            bind(o, o.v, schange, onChange);
        }

        function render() {
            opt.setSel();

            renderValContAndMenu();
        }

        function renderValContAndMenu() {
            renderValInputs();

            if (!opt.noMenu && dd.isOpen()) {
                dd.render();
            }
        }

        // don't check lrs on init
        function renderValInputs(init) {
            var res = se;
            var rawvals = awe.val(o.v);

            var vals = [];
            var itms = [];

            var lrs = glrs();

            for (var i = 0; i < rawvals.length; i++) {
                var val = rawvals[i];
                var found = 0;

                loopTrees(lrs, function (item) {
                    var itemv = toStr(item[vprop]);
                    if (val === itemv) {
                        vals.push(itemv);
                        found = 1;
                        itms.push(item);
                        return 1;
                    }
                });

                if (init || isCombo && !found && !isEmp(val) && (val.match("^" + comboPref) || !opt.multiple)) {
                    vals.push(val);
                }
            }

            if (autoSelectFirst && (!vals.length || vals.length === 1 && vals[0] === se)) {
                loopTrees(lrs, function (item) {
                    if (!item.nv) {
                        vals = [item[vprop]];
                        return 1;
                    }
                });
            }

            var attr = disbAttr(o);
            attr += ' name="' + o.nm + '"';

            loop(vals, function (value) {
                res += '<input type="' + valInputType + '" value="' + encode(value) + '" ' + attr + ' checked="checked" class="o-itmv" />';
            });

            if (!vals.length && opt.multiple) res = '<input type="checkbox" name="' + o.nm + '" class="o-itmv" />';

            $valCont.html(res);
            o.vitms = itms;
        }

        opt.init && opt.init();
        opt.prerender();

        if (hasClrBtn) {
            $odisplay.append(clrBtn);
            //clrBtn.on(sclick, function () {
            //    o.v.val(se).change();
            //});
        }

        renderValInputs(1);

        if (!opt.noMenu) {
            $odropdown.on(sclick, '.o-ddbtn', function () {
                if (!openOnHover) {
                    dd.topen();
                } else {
                    dd.open(o);
                }
            });

            o.v.on('awegdata', function (e) {
                if (ds.gc()) {
                    clearCacheOnLoad && ds.cc();
                    renderValContAndMenu();
                }
            });

            if (openOnHover) hovOpen({
                hover: $odropdown,
                open: function () {
                    dd.open();
                    return dd.cont;
                },
                close: function () {
                    dd.close();
                },
                stopf: function (e) {
                    if (trg(e).closest('.o-favb').length) return 1;
                }
            }, dd);
        }

        function changeHandler() {
            opt.setSelChange();
            dd.render();
            o.v.data('comboval', null);
        }

        o.v.on(schange, changeHandler);
        o.v.on(saweload, render);
    }

    function notif(text, time, clss) {
        var notifCont = $('#o-notifcont');

        if (!notifCont.length) {
            notifCont = $(rdiv('o-ntpc', se, 'id="o-notifcont"'));
            notifCont.appendTo($('body'));
        }

        var $popup = $(rdiv('o-ntp')).addClass(clss);
        var $content = $(rdiv('o-ntc')).html(text || 'error occured');
        var $closeBtn = $(sclosespan);

        notifCont.append($popup);
        $popup.append($content);
        $popup.append($closeBtn);
        $popup.append(rdiv('o-ntlb'));

        $closeBtn.on(sclick, function () { close(); });

        $content.css(smaxh, $win.height() - 50);

        $popup.css('opacity', .97);

        if (time) {
            setTimeout(function () {
                close(1);
            }, time);
        }

        function close(fade) {

            if (fade === 1) {
                setTimeout(function () { $popup.remove(); }, 1000);
                $popup.addClass('o-clsg');
                $popup.css('opacity', 0);
                $popup.css('margin-top', -outerh($popup, 1));
            } else {
                $popup.remove();
            }
        }

        return $popup;
    }

    function inlinePopup(o) {
        var p = o; // popup properties
        var popup = p.d; // popup div
        var wrap = $('<div class="o-inlp awe-popupw"></div>').hide();
        var cx = {};
        o.cx = cx;

        // minimum height of the lookup/multilookup content
        p.mlh = 250;

        wrap.append(popup);

        // decide where to attach the inline popup
        // tag and tags are set using .Tag(object) .Tags(string)

        var tag = o.tag;
        var target = readTag(o, "InlTrg");

        var api = function () { };
        cx.api = api;
        api.open = function (opt) {
            var opener = opt.opener || opt.e && $(opt.e.target);
            if (target) {
                $('#' + target).append(wrap);
            }
            else if (tag && tag.cont) {
                tag.cont.prepend(wrap);
            }
            else if (opener) {
                opener.after(wrap);
            } else {
                $('body').prepend(wrap);
            }

            wrap.show();
            cx.isOpen = 1;
            popup.trigger('aweopen');
        };
        api.close = function () {
            wrap.hide();
            cx.isOpen = 0;
            if (p.cl) {
                p.cl();
            }
            popup.trigger('aweclose');
            if (!p.loadOnce) {
                wrap.remove();
            }
        };
        api.destroy = function () {
            api.close();
            wrap.remove();
            awe.destroy(o);
        };

        popup.data('api', api);

        var closeBtn = $(rbtn('awe-btn', snbsp + '&times;' + snbsp)).click(api.close);

        if (readTag(o, "Sh", 1)) {
            wrap.prepend($('<div class="o-inltitl"></div>').append(closeBtn).append("<span class='o-inltxt'>" + (p.title || '') + "</span>"));
        }

        addFooter(p.btns, wrap, popup);

        return wrap;
    }

    function gridPageInfo(o) {
        var $grid = o.v;
        var $pageInfo = $('<div class="o-gpginf"></div>');
        var delta = 0;
        var $footer = $grid.find('.awe-footer');
        if (!$footer.length) return;

        $grid.on(sawerowch, function (e, data) {
            if (data) {
                delta += data;
                render();
            }
        });

        $grid.find('.awe-footer').append($pageInfo);

        $grid.on('awebfren', function (e) {
            if (!isTrgIt(e, $grid)) return;
            delta = 0;
            render();
        }).on('awepgch', function (e) {
            if (!isTrgIt(e, $grid)) return;
            render();
        });

        function render() {
            var lrs = dto($grid).lrs;
            if (!lrs) return;
            var pageSize = lrs.ps;
            var itemsCount = lrs.ic + delta;

            var first = pageSize * (o.pg - 1) + 1;
            var last = lrs.pgn ? first + pageSize - 1 + delta : itemsCount;
            if (last > itemsCount) last = itemsCount;
            if (!itemsCount || !last) first = 0;

            $grid.find('.o-gpginf').html(first + ' - ' + last + ' ' + format(cd().GridInfo, [itemsCount]));
        }
    }

    function gridPageSize(o) {
        if (isMobile()) return;

        var items = [10, 20, 50, 100];
        if (!o.ps) o.ps = 10;

        if (o.inf === 1) items = [20, 50, 100];

        function addIfLacks(ni) {
            if (!contains(ni, items)) {
                items.push(ni);
                items.sort(function (a, b) {
                    return a - b;
                });
            }
        }

        var $grid = o.v;

        var $footer = $grid.find('.awe-footer');
        if (!$footer.length) return;

        var psi = o.id + 'PageSize';

        $grid.find('.awe-footer').append('<div class="awe-ajaxradiolist-field o-gpgs awe-field"><input id="' + psi + '" class="awe-val" type="hidden" value="' + o.ps + '" /><div class="awe-display"></div></div>');

        addIfLacks(o.ps);

        function setPages() {
            return qsel(items, function (val) {
                return { c: val, k: val };
            });
        }

        awe.radioList({ id: psi, nm: psi, dataFunc: setPages, md: awem.odropdown, tag: { InLabel: cd().PageSize + ": " } });
        var psc = $('#' + psi);
        bind(o, psc, schange, function () {
            o.ps = psc.val();
            o.api.load();
        });
    }

    function scrllTo(cont, targ) {
        cont.scrollTop(cont.scrollTop() + ypos(targ) - ypos(cont));
    }

    function gridInfScroll(mopt) {
        return function (o) {
            var useBtn = mopt.moreBtn;
            var g = o.v;
            var con = g.find(sawecontentc);
            var scon = con.children().first();
            var itc = scon.find('.awe-itc:first');
            var pause;
            var toppre = 1;
            var pages;
            var loaderh = 200;
            var root = null;
            var limit = 500;

            if (o.h) root = con[0];

            if (useBtn) {
                toppre = 0;
                o.inf = 2;
            } else {
                o.inf = 1;
            }

            if (!o.ps) o.ps = 20;

            o.pgsbef = toppre;

            var observer = new IntersectionObserver(observe,
                {
                    root: root,
                    rootMargin: '350px'
                });

            var fobs = new IntersectionObserver(observe,
                {
                    root: root,
                    rootMargin: '0px'
                });

            bind(o, g, sawebeginload, function (e) {
                if (!trg(e).is(g)) return;
                pause = 1;
            });

            bind(o, g, saweload + ' aweloadstop', function (e, opt) {
                if (!trg(e).is(g)) return;

                var page = o.lrs.p;
                var add = opt && opt.setRows;
                if (!add) {
                    pages = [page];
                    if (page > 1 && toppre) pages.unshift(page - toppre);
                } else {
                    if (page > pages[0])
                        pages.push(page);
                    else
                        pages.unshift(page);
                }

                remLoaders();

                if (!add) {
                    if (page === 1) {
                        con.scrollTop(0);
                    } else {
                        var row = itc.children('.awe-row').eq(o.lrs.ps);
                        if (len(row)) {
                            scrllTo(con, row);
                        }
                    }
                }

                pause = 0;

                addBotLoader(pages[pages.length - 1] + 1, 'app');

                if (pages[0] > 1) {//&& !useBtn
                    addTopLoader(pages[0] - 1, 'pre');
                }

                renderPage();
            });

            function renderPage() {
                if (len(pages) < 2 || len(con.find('.o-loader')) > 2) return;
                var st = con.scrollTop();

                if (pages[0] !== 1) st -= loaderh;

                var p = Math.trunc(st / (scon.height() / len(pages)));

                o.pg = pages[p];
                o.api.persist();
                o.api.buildPager(o);
                g.trigger('awepgch');
            }

            var popup = con.closest('.awe-popup');
            if (len(popup)) {
                bind(o, popup, 'aweopen', renderPage);
            }

            var scrollpg = slowAct();
            bind(o, con, 'scroll', function () {
                scrollpg(renderPage, 50);
            });

            var loadPg = slowAct();
            function startLoadPage(p, act, instant) {
                if (!p) return;

                if (instant) f(); else loadPg(f, 350);

                function f() {
                    var loadOpt = {
                        page: p
                    };

                    if (act && len(itc.children()) < limit) {
                        var oprm = {};

                        var rws = itc.children('.awe-row');

                        if (act === 'app') {
                            oprm.lastKey = rws.last().data('k');
                        }

                        if (act === 'pre') {
                            oprm.firstKey = rws.first().data('k');
                        }

                        loadOpt.oparams = oprm;

                        // append gv2 to gv1
                        function mergeGroupView(gv1, gv2, key) {
                            var gs = gv1.gs;
                            var gs2 = gv2.gs;

                            if (gs) {
                                var lg = gs[len(gs) - 1];
                                var fg = gs2[0];

                                if (lg.h && fg.h && (lg.h.gkey === fg.h.gkey)) {
                                    // merge
                                    mergeGroupView(lg, fg, key);
                                    gs2.shift();
                                }

                                loop(gs2, function (itm) { gs.push(itm); });
                            }

                            if (gv1.it) {
                                var keys = {};
                                loop(gv1.it, function (itm) { keys[itm[key]] = 1; });
                                loop(gv2.it,
                                    function (itm) {
                                        if (!keys[itm[key]]) gv1.it.push(itm);
                                    });
                            }
                        }

                        var lrs = o.lrs;

                        loadOpt.setRows = function (rcon) {
                            var rs = o.lrs;
                            rs.th = Math.max(lrs.th, rs.th);

                            o.api.adjustTh(rs.th - lrs.th);

                            if (act === 'app') {
                                itc.append(rcon);

                                mergeGroupView(lrs.dt, rs.dt, rs.k);

                                rs.dt = lrs.dt;

                                mergeGr();
                            } else {
                                var sconh = scon.height();
                                itc.prepend(rcon);

                                mergeGroupView(rs.dt, lrs.dt, rs.k);

                                mergeGr();
                                var delta = scon.height() - sconh;
                                if (delta > 0) {
                                    scrollTopBy(con, delta);
                                }
                            }
                        }
                    }

                    remLoaders();
                    o.api.load(loadOpt);
                }
            }

            function observe(entries) {
                loop(entries, function (ent) {
                    if (pause || !ent.isIntersecting) return;

                    var loader = $(ent.target);
                    var p = loader.data('p');

                    var act = loader.data('act');

                    startLoadPage(p, act);

                    if (act && ent.intersectionRatio > .7) {
                        if (act === 'pre') {
                            scrollTopBy(con, loaderh);
                        }

                        return false;
                    }

                    if (len(itc.children('.o-glrow'))) return;

                    if (p > o.lrs.p && !loader.next().length) {
                        addBotLoader(p + 1);
                    }
                    else if (p > 1) {
                        addTopLoader(p - 1);
                    }

                    return false;
                });
            }

            function mergeGr(pre) {

                var ks = {};
                itc.children('.awe-ghead').each(remd);

                ks = {};
                $(itc.children('.awe-gfoot').get().reverse()).each(remd);

                ks = {};
                var rows = itc.children('.awe-row');
                if (pre) rows = $(rows.get().reverse());
                rows.each(remd);

                function remd() {
                    var itm = $(this);
                    var key = itm.data('k');
                    if (ks[key]) {
                        itm.remove();
                    }

                    ks[key] = 1;
                }
            }

            function remLoaders() {
                con.find('.o-loader').remove();
            }

            function createLoader(p, act) {
                var loader = $(rdiv('o-loader ' + act));
                if (useBtn) {
                    loader.append($(rdiv('awe-morebtn', cd().More)).click(function () {
                        startLoadPage(p, act, 1);
                    }));
                }
                else {
                    loader.addClass('o-ldrpg').append(p).height(loaderh).data('p', p).data('act', act);
                }

                return loader;
            }

            function addBotLoader(page, act) {
                if (o.lrs.pc >= page) {
                    var loader = createLoader(page, act);
                    con.append(loader);
                    !useBtn && (act ? observer : fobs).observe(loader[0]);
                }
            }

            function addTopLoader(p, act) {
                var loader = createLoader(p, act);
                con.prepend(loader);
                !useBtn && (act ? observer : fobs).observe(loader[0]);
            }
        }
    }

    function isMobileOrTablet() {
        return false;
    }

    var clientDict = {
        Empty: 'empty',
        GridInfo: "of {0} items",
        Select: 'please select',
        SearchForRes: 'search for more results',
        Search: 'search',
        NoRecFound: 'no records found',
        PageSize: 'page size',
        Months: [
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        ],
        Days: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"],
        selected: 'selected',
        More: 'more',
        Equal: 'Equal',
        LessThan: 'Less than',
        LessThanOrEqual: 'Less than or equal',
        GreaterThan: 'Greater than',
        GreaterThanOrEqual: 'Greater than or equal',
        Today: 'Today',
        Clear: 'Clear'
    };

    function gldng(disb, noEmpMsg) {
        return function (o, opt) {
            if (disb) return;
            var cspin;
            opt = opt || {};
            opt.lhtm = opt.lhtm || '<div class="spinner"><div class="dot1"></div><div class="dot2"></div></div>';
            var ctm = opt.ctm || 40;
            var g = o.v;
            var mcont = g.find('.awe-mcontent');


            function setNoRec() {
                mcont.find('.o-gempt').remove();
                if (!mcont.find(sawerowc).length) {
                    var msg = o.props["empMsg"] || cd().NoRecFound;
                    mcont.append($('<div class="o-gempt">' + msg + '</div>'));
                }
            }

            function onBeginLoad(e) {
                if (trg(e).is(g)) {
                    if (cspin) {
                        cspin.remove();
                    }

                    g.find('.o-gempt').remove();

                    var spin = $('<div class="spinCont">' + opt.lhtm + '</div>').hide();
                    spin.height(mcont.height());
                    mcont.prepend(spin);
                    //spin.children().first().css('margin-top', mcont.height() / 2 - ctm + 'px');

                    //spin.delay(1).fadeIn();

                    spin.show();
                    spin.addClass('on');
                    //spin.css('opacity', '1');                    
                    cspin = spin;
                }
            }

            function onLoad(e) {
                if (trg(e).is(g)) {
                    mcont.find('.spinCont:first').fadeOut().remove();

                    if (!noEmpMsg) {
                        setNoRec();
                    }
                    cspin = null;
                }
            }

            bind(o, g, sawebeginload, onBeginLoad);
            bind(o, g, saweload, onLoad);

            if (!noEmpMsg) {
                g.on(sawerowch, setNoRec);
            }
        };
    }

    function gridLoading(o, opt) {
        gldng()(o, opt);
    }

    function gridFreezeColumns2(opt) {
        return function (o) {
            var g = o.v;
            var gid = '[data-g=' + o.id + ']';
            var mcont = g.find('.awe-mcontent' + gid);
            var cont = mcont.find('.awe-content').first();
            var acont = cont.add(mcont.find('.awe-gfc').first());

            var fzo = {};
            var api = dapi(g);
            var ich = api.ich;
            var getw = api.colw;
            var contEl = cont[0];
            var prevSl = cont.scrollLeft();
            var isReset = 0;
            var adjust;

            var hrow = g.find('.awe-hrow');

            var sleft = 'left';
            var sright = 'right';

            function setlr() {
                if (o.rtl) {
                    sright = 'left';
                    sleft = 'right';
                }
            }

            o.fzc = 1;

            bind(o, g, 'aweheaderinit', reset);

            bind(o, g, 'aweupdate aweinlineedit aweinlinecancel aweinlinesave awenestload awecolresizeend awerender',
                function () {
                    reset();
                    onScroll(cont.scrollLeft());
                });

            bind(o, cont, 'scroll', function (e) {
                onScroll(e.target.scrollLeft);
            });

            bind(o, g, 'aweinitlay', function () {
                onScroll(cont.scrollLeft(), 1);
            });

            bind(o, $win, 'resize awedomlay', function () {
                resetGh();
                onScroll(cont.scrollLeft(), 1);
                adjust && adjust();
            });

            bind(o, g, 'awecolresize', function (e) {
                resetGh();
                onScroll(cont.scrollLeft(), 1);
            });

            function freezeCol(lcount, rcount, lwidths, rwidths) {
                if (fzo.rc === rcount && fzo.lc === lcount) return;

                fzo.rc = rcount;
                fzo.lc = lcount;

                var cls = 'o-sfz';
                var remcls = cls + ' o-fzrb';
                setlr();

                function rowf(_, el, hcells) {
                    // set class and side to cells
                    var accw = 0;
                    var j = 0;
                    var cells = hcells || $(el).children();

                    loop(cells, function (cell) {
                        cell = $(cell);
                        var isIdn = cell.hasClass('awe-idn');

                        if (j < lcount) {
                            cell.addClass(cls).css(sleft, accw);

                            accw += (isIdn ? 16 : lwidths[j]);

                            if (j === lcount - 1) {
                                cell.addClass('o-fzlb');
                            }
                        } else {
                            cell.removeClass(remcls).css(sleft, se).css(sright, se);
                        }

                        if (!isIdn) {
                            j++;
                        }
                    });

                    accw = 0;
                    j = 0;
                    var rc = rcount;
                    for (var l = cells.length - 1; rc; l--, rc--) {
                        var td = $(cells[l]);
                        td.addClass(cls).css(sright, accw);

                        accw += rwidths[j];

                        if (rc === 1) {
                            td.addClass('o-fzrb');
                        }
                        j++;
                    }
                }

                var itcs = acont.find('.awe-itc:first');

                var scols = hrow.find('tr:not(.awe-row) td:not(.awe-hgc)');

                scols = scols.sort(function (a, b) {
                    return $(a).data('i') - $(b).data('i');
                });

                rowf(0, 0, scols);

                hrow.find(sawerowc).each(rowf);

                itcs.children(sawerowc).each(rowf);

                adjust = function () {
                    var clientWidth = contEl.clientWidth;
                    itcs.children('.awe-ghead,.awe-nest').each(function (_, el) {
                        var accw = 0;
                        var j = 0;
                        var cells = $(el).children();

                        for (var k = 0; k < cells.length && j <= lcount - 1; k++) {
                            var td = $(cells[k]);

                            if (!td.hasClass('awe-idn')) {
                                j++;
                                if (k === cells.length - 1) {
                                    var pad = outerw(td) - td.width();
                                    var ghcon = td.find('.awe-ghcon,.o-fzcon');
                                    ghcon.addClass(cls);
                                    ghcon.css(sleft, accw + pad).outerWidth(clientWidth - accw - 100);
                                }
                            } else {
                                td.addClass(cls);
                                td.css(sleft, accw);
                            }

                            accw += outerw(td);
                        }
                    });
                }

                adjust();
            }

            function restore() {
                fzo = {};
                var cls = 'o-sfz o-fzrb o-fzlb';
                acont.find(sawerowc).add(hrow).find('.o-sfz').removeClass(cls).css(sleft, se).css(sright, se);
            }

            function reset() {
                prevSl = 0;
                isReset = 1;
                restore();
            }

            function resetGh() {
                hrow.find('.awe-hgc').each(function (_, ghc) {
                    ghc = $(ghc);
                    ghc.removeClass('o-sfz');
                    ghc.find('.o-fzhgw').each(function (__, wc) {
                        wc = $(wc);
                        wc.after(wc.html());
                        wc.remove();
                    });
                });
            }

            function onScroll(sl, force) {
                var leftCount = opt.left;
                var rightCount = opt.right;
                var delta = sl - prevSl;
                var maxScrollLeft = contEl.scrollWidth - contEl.clientWidth;

                if ((delta || isReset) && (leftCount || rightCount) || force) {
                    if (!maxScrollLeft) {
                        restore();
                        return;
                    }

                    isReset = 0;
                    prevSl = sl;

                    var scrlsp = opt.minsw || 200;

                    var columns = o.columns;

                    var minw = (o.th + o.gl) * 16;
                    var avcontw = contEl.clientWidth - minw;
                    var lcount = 0;
                    var clen = columns.length;

                    var lwidths = [];
                    var rwidths = [];
                    for (var i = 0; i < clen && lcount < leftCount; i++) {
                        var lcol = columns[i];
                        if (!ich(lcol)) {
                            var defw = getw(lcol);
                            if (avcontw - defw < scrlsp) break;
                            avcontw -= defw;
                            lwidths.push(defw);
                            lcount++;
                        }
                    }

                    var rcount = 0;
                    for (var j = clen - 1; j > 0 && rcount < rightCount; j--) {
                        var rcol = columns[j];
                        if (!ich(rcol)) {
                            var defw = getw(rcol);
                            if (avcontw - defw < scrlsp) break;
                            avcontw -= defw;
                            rwidths.push(defw);
                            rcount++;
                        }
                    }

                    if (lcount || rcount) {
                        freezeCol(lcount, rcount, lwidths, rwidths);
                    }

                    var ri = clen - rcount;

                    if (o.rtl) {
                        sl *= -1;
                    }

                    var slr = maxScrollLeft - sl;
                    setlr();

                    hrow.find('.awe-hgc').each(function (_, ghc) {
                        ghc = $(ghc);
                        var ghi = ghc.data('i');
                        var colspan = parseInt(ghc.attr('colspan'), 10);
                        var li = ghi + colspan - 1;
                        var isLeft = ghi < lcount;
                        var isRight = rcount && li >= ri;

                        if (isLeft || isRight) {
                            var colb = hrow.find('.awe-hc[data-i=' + ghi + ']');
                            var cole = hrow.find('.awe-hc[data-i=' + li + ']');

                            var ghcon = ghc.find('.o-fzhgw');

                            if (!ghc.hasClass('o-sfz')) {
                                ghc.height(ghc.height());
                                ghc.addClass('o-sfz');

                                // wrap awe-col
                                if (!ghcon.length) {
                                    ghcon = $('<div class="o-fzhgw"/>');
                                    if (isRight) ghcon.css('float', sright);
                                    ghc.append(ghcon);
                                    ghcon.append(ghc.find('.awe-col'));
                                }
                            } else {
                                ghcon = ghc.children();
                            }

                            if (isLeft) {
                                ghc.css(sleft, colb.css(sleft));
                            } else {
                                ghc.css(sright, cole.css(sright));
                            }

                            if (isLeft && !isRight || isRight) {
                                var smvar = 0;
                                var smcon = 0;
                                var bord = isLeft ? lcount - 1 : ri;
                                for (var y = ghi; y <= li; y++) {
                                    var col = columns[y];
                                    var cw = getw(col);
                                    if (isLeft && y <= bord || isRight && y >= bord) {
                                        smcon += cw;
                                    } else {
                                        smvar += cw;
                                    }
                                }

                                ghcon.outerWidth(Math.min(avcontw + smcon,
                                    smcon + Math.max(0, smvar - (isRight ? slr : sl))));
                            }
                        } else {
                            ghc.css(sleft, '').css(sright, '');
                        }
                    });

                } // end if delta
            }
        }
    }

    function gridMovRows(opt) {
        return function (o) {
            var $grid = o.v;
            var $fromCont = $grid.find(sawecontentc);
            var gridsIds = [o.v.attr('id')];
            var isOn;
            var curhov;
            var drgo;
            var model;

            var feat = { apply: apply };

            function clgapi(of) {
                return dapi(of.closest(sawegridcls));
            }

            function apply() {
                if (isOn) return;
                var scroll = [];
                if (opt && opt.G) {
                    gridsIds = opt.G;
                }

                scroll.push({ c: $win, y: 1 });

                function newo(cx) {
                    clgapi(drgo).rem(drgo);
                    return $(clgapi(cx.cont).renderRow(model));
                }

                function ondrop(cx) {
                    var $toGrid = cx.cont.closest(sawegridcls);
                    if (!$toGrid.is($grid)) {
                        movedGridRow($grid, $toGrid);
                    }
                }

                function swrap(cx) {
                    curhov = 0;
                    drgo = cx.drgo;
                    model = dapi($grid).model(drgo);

                    // create dragobj
                    if (drgo.closest('.awe-itc').is('tbody')) {
                        return $('<div/>')
                            .prop('class', $grid.prop('class'))
                            .append($('<table/>')
                                .append(drgo.closest('table').find('colgroup').clone())
                                .append(drgo.clone()));
                    }
                }

                function shov(cx) {
                    if (curhov !== cx.cont) {
                        cx.plh.remove();

                        curhov = cx.cont;
                        cx.plh = $(clgapi(curhov).renderRow(model)).addClass('awe-hl');
                    }
                }

                var remf = dragReor({
                    from: $fromCont.find('.awe-itc:first'),
                    tof: function () {
                        var gobjs = [];
                        loop(gridsIds, function (val) {
                            var tog = $('#' + val).find(sawecontentc);
                            scroll.push({ c: tog, y: 1 });
                            gobjs.push(tog);
                        });

                        return gobjs;
                    },
                    sel: '.awe-row',
                    plh: 'awe-hl',
                    scroll: scroll,
                    newo: newo, // gen new obj to append
                    ondrop: ondrop,
                    hovec: function (cx) {
                        // hovering cont
                        cx.cont.find('.awe-itc:first').append(cx.plh);
                    },
                    cancel: function (cx) {
                        return awe.dgcf($fromCont)(cx) || cx.drgo.closest('.o-glrow').length;
                    },
                    swrap: swrap,
                    shov: shov
                });

                feat.rem = function () {
                    if (isOn) {
                        remf();
                        isOn = 0;
                    }
                };

                isOn = 1;
            }

            o.api.ft['mvr'] = feat;

            apply();

            feat.apply = apply;
        };
    }

    function getColValCont(column, row) {
        var colgroup = row.parent().parent().find('colgroup:first');

        if (!len(colgroup)) {
            return row.find('.awe-cv[data-i="' + column.ix + '"]');
        }

        var tdi = colgroup.find('col[data-i="' + column.ix + '"]').index();

        var rvi = len(colgroup.children()) - tdi - 1;

        return $(row.children().get().reverse()[rvi]);
    }

    function gridInlineEdit(createUrl, editUrl, oneRow, reloadOnSave, batch, rowClick, dataFunc, initRow) {
        var slock = 'slock';
        var co = dataFunc || {};
        co.save = co.save || co.create;
        co.edit = co.edit || co.save;
        editUrl = editUrl || createUrl;

        function vldClsName(prop) {
            return 'ovld-' + toLowerFirst(prop);
        }

        return function (o) {
            var g = o.v;
            var api = dapi(g);
            var newic = 1;
            var activeRow;

            function oneRowCheck(action) {
                if (!oneRow) return;

                var otherRow = g.find(sglrowc).first();

                if (!otherRow.length) return;

                if (otherRow.data(slock)) {
                    return 1;
                } else {
                    cancelRow(otherRow);
                    action();
                    return 1;
                }
            }

            api.inlineCreate = function inlineCreate(newModel, opt) {
                if (oneRowCheck(function () { inlineCreate(newModel); })) {
                    return;
                }
                var lvl = 1;
                var parent = opt && opt.parent;
                if (parent) {
                    lvl = parent.data('lvl') + 1;

                    var lastth = o.th;
                    var newth = lvl - o.gl - 1;
                    o.th = Math.max(lastth, newth);
                    api.adjustTh(o.th - lastth);
                }

                newModel = newModel || {};

                var nitm = $(api.renderRow(newModel, lvl));
                nitm.addClass(snewrow);

                if (parent) {
                    parent.after(nitm.data('inlp', parent));
                } else {
                    g.find('.awe-content:first .awe-itc:first').prepend(nitm);
                }

                var nxt = nitm.next();
                len(nxt) && !nxt.hasClass('awe-alt') && nitm.addClass('awe-alt');
                g.trigger(sawerowch);
                inlineEdit(nitm);
            };

            api.inlineEdit = inlineEdit;
            api.inlineCancel = function ($row, focus) { cancelRow($row, focus); };
            api.inlineCancelAll = function () { g.find('.o-glrow').each(function () { cancelRow($(this)); }) };
            api.inlineSave = save;
            api.batchSave = batchSave;

            function isMine(tg) {
                return tg.closest(sawegridcls).is(g);
            }

            g.on(sclick, '.o-glsvb', function () {
                var tg = $(this);
                if (isMine(tg)) {
                    save(tg.closest(sawerowc), 1);
                }
            })
                .on(sclick, '.o-glcanb', function () {
                    var tg = $(this);
                    if (isMine(tg)) {
                        api.inlineCancel(tg.closest(sawerowc), 1);
                    }
                })
                .on(sclick, '.o-gledtb', function () {
                    var tg = $(this);
                    if (isMine(tg)) {
                        inlineEdit(tg.closest(sawerowc));
                    }
                });

            function inlineEdit(row, td) {
                if (oneRowCheck(function () { inlineEdit(row, td); })) {
                    return;
                }

                activeRow = row;

                row.addClass(sglrow + ' awe-nonselect awe-nonceb');

                var model = api.model(row);

                var hidden = se;
                var prefix = o.id + (model[o.k] || se);

                if (row.hasClass(snewrow)) {
                    prefix += 'new' + newic++;
                }

                var idFunc = function (strid) {
                    return prefix + strid;
                };

                var gutil = gu({ prefix: prefix, o: o, model: model });
                var getVal = gutil.getVal;
                var parseFormat = gutil.parse;

                var cx = { id: idFunc, model: model, val: getVal, grido: o };

                var strInitFuncs = [];
                var initFuncs = [];

                loop(o.columns, function (column) {
                    var tag = column.Tag || column.tag;
                    if (tag) {

                        var inlElms = tag.Format || tag.inline;

                        if (tag.inlinef) {
                            inlElms = tag.inlinef(cx);
                        }
                        else if (tag.FormatFunc) {
                            inlElms = eval(tag.FormatFunc)(model, tag.Format, cx);
                        }

                        if (typeof inlElms === 'function') {
                            inlElms = inlElms(cx);
                        }

                        inlElms = ensureArray(inlElms);

                        if (tag.inline || tag.inlinef) {
                            parseElmsFromUi(inlElms, column, cx);
                        }

                        if (len(inlElms)) {
                            var gtd = getColValCont(column, row);
                            if (!column.Hid) {
                                gtd.empty();
                            }

                            loop(inlElms, function (inlEl) {
                                var val = getVal(inlEl.valProp);

                                var inlHtml = inlEl.html && inlEl.html(cx) || parseFormat(inlEl.format, val);

                                if (column.Hid) {
                                    hidden += inlHtml;
                                } else {
                                    var prop = inlEl.modelProp;
                                    var validstr =
                                        prop &&
                                            inlHtml.indexOf(sgvalidmsg) === -1 ?
                                            rdiv(sgvalidmsg + ' ' + vldClsName(prop), se, 'vld-for="' + prop + '"') :
                                            se;

                                    var cellcont = rdiv('oinlc', rdiv('oinle', inlHtml) + validstr, 'data-name="' + prop + '"');
                                    addHidden(gtd.append(cellcont));
                                }

                                if (inlEl.jsFormat) {
                                    strInitFuncs.push(parseFormat(inlEl.jsFormat, val));
                                }
                                else if (inlEl.init) {
                                    initFuncs.push(inlEl.init);
                                }
                            });
                        }
                    }
                });

                if (hidden) {
                    addHidden(row.children().last());
                }

                function addHidden(cont) {
                    if (hidden) {
                        cont.append($('<div>' + hidden + '</div>').hide());
                        hidden = se;
                    }
                }

                loop(strInitFuncs, function (sf) {
                    eval(sf);
                });

                loop(initFuncs, function (f) {
                    f(cx);
                });

                var inState = serializeAllForm(row);

                row.data('ins', inState);
                row.trigger(saweinledit);

                // init(row, td) 
                initRow && initRow(row, td);

                var selfunc = function (elm) { return tabbable(elm).first(); };

                // focus first input
                if (!isMobile()) {
                    setTimeout(function () {
                        if (td && selfunc(td).length) {
                            selfunc(td).focus();
                        } else {
                            // no focus when hor scrollbar
                            var gcont = g.find('.awe-content');
                            if (gcont.children().first().width() <= gcont.width()) {
                                selfunc(row).focus();
                            }
                        }
                    });
                }

                if (rowClick) {
                    setTimeout(function () {
                        regOutClick(row);
                    });
                }
            }

            function cancelUnchangedRow(row, isClick) {
                // check for changes
                var currState = serializeAllForm(row);
                var same = 1;
                var ins = row.data('ins');

                if (row.hasClass(snewrow) || (len(ins) !== len(currState))) {
                    same = 0;
                } else {
                    for (var i = 0; i < ins.length; i++) {
                        if (ins[i].name !== currState[i].name || ins[i].value !== currState[i].value) {
                            same = 0;
                            break;
                        }
                    }
                }

                if (same) {
                    cancelRow(row, isClick);
                }

                return same;
            }

            function getSaveLock(row) {
                if (row.data(slock)) {
                    return;
                }

                row.data(slock, 1);
                return 1;
            }

            function remRowLock(row) {
                row.data(slock, 0);
            }

            function clientInvRow(evd) {
                $(evd.target).closest('.awe-row').trigger(saweinlinv);
            }

            function batchSave(prows) {
                var acc = 0;
                var res = [];
                var rows = [];

                var evd = {};

                var checkVldElm = g;

                // click on row save btn
                if (len(prows) == 1) {
                    checkVldElm = $(prows[0]);
                }

                checkVldElm.trigger(saweinlbfsave, evd);
                if (evd.cancel) {
                    clientInvRow(evd);
                    return;
                }

                loop((prows || g.find('.o-glrow').get()), function (row) {
                    row = $(row);
                    if (getSaveLock(row) && !cancelUnchangedRow(row)) {
                        var sdata = row.find(':input').serializeArray();
                        res = res.concat(qsel(sdata,
                            function (nv) {
                                nv.name = 'inputs[' + acc + '].' + nv.name;
                                return nv;
                            }));

                        rows.push(row);
                        acc++;
                    }
                });

                if (!len(res)) return 1;


                var opt = {
                    o: o,
                    data: res.concat(awe.params(o, 1)),
                    success: onSuccess,
                    complete: onComplete,
                    error: onError,
                    url: createUrl,
                    func: co.save
                }

                o.lrso = 1;

                g.trigger(sawebeginload);

                return awe.guf(opt);

                function onError(p1, p2, p3) {
                    awe.err(o, p1, p2, p3);
                }

                function onComplete() {
                    g.trigger(saweload);
                    loop(rows, function (row) {
                        remRowLock(row);
                    });
                }

                function onSuccess(data) {
                    if (len(data) !== len(rows)) {
                        onError('result length mismatch');
                    }

                    loop(data, function (rowRes, i) {
                        onRowSave(rowRes, rows[i]);
                    });
                }
            }

            function onRowSave(res, row, isClick) {
                var valid = 1;
                row.find('.' + sgvalidmsg).empty();
                var errors = res._errs;
                if (errors) {
                    valid = 0;

                    for (var fldName in errors) {
                        var msg = se;
                        loop(errors[fldName], function (cerr) {
                            msg += rdiv('field-validation-error', cerr);
                        });

                        if (!fldName || !row.find('.' + vldClsName(fldName)).length) {
                            row.find('.' + sgvalidmsg + ':last').append(msg);
                        } else {
                            row.find('.' + vldClsName(fldName)).html(msg);
                        }
                    }

                    row.trigger(saweinlinv);
                } else {
                    var ritem = res.Item || res.item;

                    awe.destroyCont(row);

                    if (!reloadOnSave) {
                        var parent = row.data('inlp');
                        if (parent) {
                            var leaf = parent.find('.awe-lf');
                            if (len(leaf)) {
                                leaf.prepend(api.ceb()).removeClass('awe-lf');
                                leaf.addClass('awe-ceb');
                                parent.addClass('awe-ghead');
                            }
                        }

                        if (ritem) {
                            // new
                            var nrow = replaceRow(row, ritem);

                            //awe.flash(nrow);

                            closeActiveRow(row);
                            nrow.trigger(saweinlsave, { r: res });
                        } else {
                            // edit
                            var item = api.model(row);
                            var key = o.k;

                            $.when(api.update(item[key], 0, 1)).done(function () {
                                closeActiveRow(row);
                                var urow = api.select(item[key])[0];
                                if (isClick) focusRowEditBtn(urow);
                                urow.trigger(saweinlsave, { r: res });
                            });
                        }
                    }
                }

                return valid;
            }

            function save(erow, isClick) {
                if (batch) return batchSave([erow]);

                var evd = {};
                erow.trigger(saweinlbfsave, evd);
                if (evd.cancel) {
                    clientInvRow(evd);
                    return;
                }

                if (!getSaveLock(erow)) return;

                if (cancelUnchangedRow(erow, isClick)) {
                    return 1;
                }

                o.lrso = 1;

                var sdata = erow.find(':input').serializeArray();
                var opt = {
                    o: o,
                    data: sdata.concat(awe.params(o, 1)),
                    success: onSuccess,
                    complete: onComplete,
                    error: onError
                }

                if (erow.hasClass(snewrow)) {
                    opt.url = createUrl;
                    opt.func = co.save;
                } else {
                    opt.url = editUrl;
                    opt.func = co.edit;
                }

                return awe.guf(opt);

                function onError(p1, p2, p3) {
                    awe.err(o, p1, p2, p3);
                }

                function onComplete() {
                    remRowLock(erow);
                }

                function onSuccess(rdata) {
                    if (onRowSave(rdata, erow, isClick)) {
                        reloadOnSave && api.load();
                    }
                }
            }

            function replaceRow(row, item) {
                var ghead = row.hasClass('awe-ghead');
                var nrow = $(api.renderRow(item, row.data('lvl'), row.data('i'), ghead));
                if (ghead) {
                    nrow.addClass('awe-ghead');
                }
                if (row.hasClass('awe-alt')) nrow.addClass('awe-alt');
                row.after(nrow).remove();
                return nrow;
            }

            function cancelRow(row, isClick) {
                awe.destroyCont(row);

                if (row.hasClass(snewrow)) {
                    g.trigger(saweinlcancel);
                    row.remove();
                } else {
                    var item = api.model(row);
                    var nrow = replaceRow(row, item);

                    if (isClick) {
                        focusRowEditBtn(nrow);
                    }

                    nrow.trigger(saweinlcancel);
                }

                g.trigger(sawerowch);
                closeActiveRow(row);
            }

            function closeActiveRow($row) {
                if (activeRow && (activeRow.is($row) || !activeRow.closest(document).length)) {
                    activeRow = null;

                    if (g.find(sglrowc).length) {
                        activeRow = g.find(sglrowc).first();
                    }
                }
            }

            if (rowClick) {
                bind(o, g, 'click keyup', '.awe-row:not(.o-glar)', function (e) {
                    var row = $(this);
                    if (!isMine(row)) return;

                    var cell = trg(e).closest('td, .awe-cv');

                    if ((istrg(e, 'button')) && !row.hasClass(sglrow) ||
                        istrg(e, '.o-glnon') ||
                        !row.closest(document).length) {
                        return;
                    }

                    activeRow = row;
                    if (!row.hasClass(sglrow)) {
                        inlineEdit(row, cell);
                    } else {
                        regOutClick(row); // vld rows
                    }
                });
            }

            function regOutClick(row) {
                if (row.data('aweroc')) return; // reg on click flag
                row.data('aweroc', 1);

                if (row.hasClass('o-glar')) return;

                row.addClass('o-glar');

                row.on(skeydown, onKeyDown);

                var tg;
                function mouseDown(e) {
                    tg = trg(e);
                }

                function mouseUp(e) {
                    // row, target
                    function lookFor(src, pivot) {
                        if (pivot.closest(src).length) {
                            return 1;
                        }

                        // multirem, child odropdown item
                        var apid = pivot.attr('awepid');
                        if (apid) {
                            return lookFor(src, $('#' + apid));
                        }

                        var popup = pivot.closest('.o-pu');

                        if (popup.length) {
                            var popener = kvIdOpener[popup.data('i')];
                            if (popener) {
                                return lookFor(src, popener);
                            }
                        }
                    }

                    var ltrg = tg || trg(e); // on return to vld row trg = null 
                    if (!$(row).closest(document).length) {
                        deregOutclick();
                    } else {
                        var isInDom = ltrg.closest(document).length;
                        var foundRowAsParent = lookFor(row, ltrg);
                        if (isInDom && !foundRowAsParent) {
                            if (batch) {
                                cancelUnchangedRow(row);
                            } else {
                                save(row);
                            }

                            deregOutclick();
                            row.removeClass('o-glar');
                        }
                    }
                }

                function deregOutclick() {
                    unbind(o, mouseUp);
                    unbind(o, mouseDown);

                    row.off(skeydown, onKeyDown);
                    row.data('aweroc', 0);
                }

                // tab nav edit through rows
                function onKeyDown(e) {
                    var sel = 'td:first, .awe-cv:first';
                    if (which(e) === keyTab) {
                        var tabls = tabbable(row);
                        if (trg(e).is(tabls.last()) && !e.shiftKey) {
                            prevDef(e);
                            row.next().find(sel).click();
                        }
                        else if (trg(e).is(tabls.first()) && e.shiftKey) {
                            prevDef(e);
                            row.prev().find(sel).click();
                        }
                    }
                }

                bind(o, $doc, 'mousedown', mouseDown);
                bind(o, $doc, 'click mouseup', mouseUp);
            }

            function scrollToRow(row) {
                scrollTo(row, g.find(sawecontentc));
            }

            function onGridBeforeLoad(e, aobj) {
                if (!isTrgIt(e, g) || aobj.opt.setRows) return;

                if (len(g.find(sglrowc))) {
                    aobj.load = null;
                    g.trigger('aweloadstop');
                    scrollToRow(activeRow);
                }
            }

            if (rowClick && !batch) {
                bind(o, g, 'awebeforeload', onGridBeforeLoad);
            }

            function focusRowEditBtn(row) {
                row.find('.o-gledtb').focus();
            }
        };
    }

    function gridColAutohide(o) {
        // 1 or 0 if not autohide
        function autohidVal(col) {
            var v = readTag(col, "Autohide", false);
            return v;
        }

        var grid = o.v;
        var api = dapi(grid);
        var colw = api.colw;
        var ich = api.ich;

        function autohideColumns(isInit) {
            var changes = 0;
            var avw = grid.find('.awe-hcon').width() || grid.find(sawecontentc).width() - awe.scrollw();
            var eo = dto(grid);

            if (avw < 0) return changes;

            var ahcols = where(eo.columns, function (col) {
                return autohidVal(col);
            }).sort(function (a, b) { return autohidVal(b) - autohidVal(a); }).reverse();

            // unhide autohidden
            loop(ahcols, function (col) {
                if (col.Hid === 2) {
                    col.Hid = 0;
                    changes++;
                }
            });

            var contentWidth = o.api.conw();
            if (avw < contentWidth) {
                loop(ahcols, function (col) {
                    if (!ich(col) && !col.Uhid) {
                        col.Hid = 2;
                        changes--;
                        contentWidth -= colw(col);
                        if (contentWidth <= avw) return false;
                    }
                });
            }

            if (changes) {
                if (!isInit && !grid.find('.awe-nest:visible').length) {
                    dapi(grid).render();
                }

                grid.trigger(sawecolschange);
            }

            return changes;
        }

        grid.on(saweinit, function (e) {
            if (trg(e).is(grid)) {
                autohideColumns(1);
            }
        });

        var gw = grid.width();
        function resizeHandler() {
            checkObjPres(o, grid, resizeHandler);
            var ngw = grid.width();
            var delta = Math.abs(gw - ngw);
            if (delta > 10) {
                gw = ngw;
                autohideColumns();
            }
        }

        bind(o, $win, 'aweresize resize awedomlay', resizeHandler);
    }

    function checkObjPres(o, obj, hand) {
        if (!obj.closest('body').length) {
            unbind(o, hand);
        }
    }

    function gridColSel(o) {
        var $grid = o.v;
        var scid = o.id + 'ColSel';

        var footer = $grid.find('.awe-footer');
        if (!len(footer)) return;
        footer.append(lsted(scid, '', 'o-gcolsl', scid, 1));

        function getColumnsDataFunc() {
            var result = [];
            loop(o.columns, function (col, i) {
                if (!readTag(col, "Nohide")) {
                    var name = readTag(col, "Caption") || col.H || col.P || "col" + (i + 1);
                    result.push({ k: i, c: name });
                }
            });

            return result;
        }

        var so = '<i class="o-o"></i>';

        awe.checkboxList({ id: scid, nm: scid, dataFunc: getColumnsDataFunc, md: awem.multiselb, tag: { InLabel: so + so + so, NoSelClose: 1 } });
        var colSel = $('#' + scid);

        function setItems() {
            var selColIndx = []; // value
            loop(o.columns,
                function (col, i) {
                    if (!col.Hid && !readTag(col, "Nohide")) selColIndx.push(i);
                });

            colSel.val(JSON.stringify(selColIndx));
            dapi(colSel).load();
        }

        bind(o, $grid, saweinit + ' ' + sawecolschange + ' ' + saweload + ' awereorder', function (e, d) {
            if (trg(e).is($grid) && !(d && d.c)) {
                setItems();
            }
        });

        colSel.on(schange, function () {
            var colIndxs = $.parseJSON($(this).val() || "[]");
            loop(o.columns, function (col, i) {
                if ($.inArray(i.toString(), colIndxs) === -1 && !readTag(col, "Nohide")) {
                    if (!col.Hid) {
                        col.Hid = 1; // hide column

                        if (col.Gd) {
                            // remove grouped when hiding column
                            col.Gd = 0;
                            o.lrso = 1;
                        }
                    }
                } else {
                    if (col.Hid) {
                        col.Hid = 0;
                        col.Uhid = 1;
                    }
                }
            });

            var api = dapi($grid);
            api.persist();
            api.render();
            $grid.trigger(sawecolschange, { c: 1 });
        });
    }

    function gridMiniPager(o) {
        return gridAutoMiniPager(o, 1);
    }

    function parseElmsFromUi(elms, column, cx) {
        var res = [];
        loop(elms, function (elm) {
            var eprop = elm.modelProp || elm.valProp || column.P;

            if (elm.func) {
                elm.init = function () {
                    var opt = elm.opt || {};
                    if (typeof opt === 'function') {
                        opt = opt(cx);
                    }

                    opt.id = cx.id(eprop);

                    elm.func(opt);
                }

                var type = 'hidden';

                if (elm.func === awem.aweui.datepicker) {
                    type = 'text';
                }

                elm.format = '<input type="' + type + '" id="#Prefix' + eprop + '" value="#Value" name="' + eprop + '" />';
            }

            elm.valProp = elm.valProp || eprop;
            elm.modelProp = eprop;
            res.push(elm);
        });

        return res;
    }

    function loadgflt(o) {
        var opt = o.fltopt;
        var cont = opt.cont;
        var model = opt.model || {};

        var inp = opt.inp || cont.find(':input');
        var nmodel = serializaInpAsObj(inp);
        if (!areScritEq(model, nmodel)) {
            var res = inp.serializeArray();
            model = nmodel;

            var norder = [];
            loop(opt.order, function (name) {
                if (isNotNull(model[name])) {
                    norder.push(name);
                }
            });

            for (var nom in model) {
                if ($.inArray(nom, norder) === -1) {
                    norder.push(nom);
                }
            }

            opt.order = norder;
            opt.model = model;

            res = res.concat(awef.serlArr(norder, 'forder'));

            o.fparams = res;

            o.api.load();
        }

        cont && cont.children().each(function (_, c) {
            var hasVal = false;
            var fc = $(c);
            fc.find('.awe-val').each(function (__, inp) {
                if (!$(inp).closest('.o-op').length && $(inp).val()) {
                    hasVal = true;
                }
            });

            if (hasVal) {
                fc.addClass('o-hv');
            } else {
                fc.removeClass('o-hv');
            }
        });
    }

    function gridFilterRow(opt) {
        return function (o) {
            var grid = o.v;
            var gid = o.id;
            var row;

            var fopt = { model: {} };
            o.fltopt = fopt;

            var loadHandls = [];
            var keyupsel = '.o-instch';
            var clearbtns = true;

            if (opt) {
                if (opt.keyupsel) keyupsel += ', ' + opt.keyupsel;
                if (opt.clearBtns === false) clearbtns = false;
            }

            o.api.freset = function () {
                o.fparams = null;
                fopt.model = {};
                fopt.order = [];
                return o.api.load({ rhead: 1 });
            }

            bind(o, grid, 'click', '.o-fcell .o-clrAll', function () {
                $(this).closest('.o-fcell').find('input[name]').each(function () {
                    $(this).val('').change();
                });
            });

            bind(o,
                grid,
                'change keyup',
                '.o-frow',
                function (e) {
                    var tg = trg(e);
                    if (e.type === 'keyup' && tg.is(keyupsel)) {
                        load();
                    } else if (e.type === schange && !tg.is(keyupsel)) {
                        load();
                    }
                });


            function load() {
                fopt.cont = row;
                loadgflt(o);
            }

            bind(o, grid, 'aweinit', function () {
                var res = awef.serlObj(fopt.model);
                res = res.concat(awef.serlArr(fopt.order, 'forder'));
                o.fparams = res;
            });

            bind(o, grid, 'awerender', load);

            bind(o,
                grid,
                'awereset',
                function () {
                    o.fparams = null;
                    fopt.model = {};
                    fopt.order = [];
                });

            bind(o, grid, 'aweheaderinit', function () {
                if (grid.find('.o-frow').length) return;

                loop(loadHandls,
                    function (h) {
                        grid.off(saweload, h);
                    });

                loadHandls = [];

                row = $(o.api.renderRow({}));
                row.children().each(function (_, el) { $(el).empty() });

                row.addClass('o-frow awe-nonselect');
                grid.find('.awe-hrow').append(row);

                onGridLoad(function () {
                    row.find('.awe-val').each(function (e) {
                        var elm = $(this);
                        if (len(elm.closest('.o-nfload'))) return;

                        var api = dapi($(this));
                        if (api && api.load) api.load();
                    });
                });

                var frow = o.lrs.tg ? o.lrs.tg.frow : {};

                var prefix = gid + 'flt';

                var idFunc = function (strid) {
                    return prefix + strid;
                };

                var gutil = gu({ o: o, model: fopt.model, prefix: prefix });
                var parseFormat = gutil.parse;
                var getVal = gutil.getVal;

                var strInitFuncs = [];
                var initFuncs = [];

                var cx = {
                    id: idFunc, model: fopt.model, val: getVal, grido: o
                };

                loop(o.columns,
                    function (col) {

                        if (col.Hid) {
                            return;
                        }

                        var fchtml = '';

                        var gtd = getColValCont(col, row);
                        gtd.empty();

                        var prop = col.P;
                        cx.bind = prop;

                        var ctag = col.Tag || col.tag;
                        var elms = null;
                        var elen = 0;
                        if (ctag) {
                            elms = ctag.Filters || ctag.filter;

                            if (typeof elms === 'function') {
                                elms = elms(cx);
                            }

                            elms = ensureArray(elms);

                            elms = parseElmsFromUi(elms, col, cx);
                            elen = elms.length;
                            loop(elms,
                                function (elm) {
                                    var val = getVal(elm.valProp);
                                    var inlHtml = elm.html && elm.html(cx) || parseFormat(elm.format, val);

                                    if (!inlHtml) {
                                        defaultFilter(elm.valProp, elm);
                                    } else {
                                        if (elm.jsFormat) {
                                            strInitFuncs.push(parseFormat(elm.jsFormat, val));
                                        } else if (elm.init) {
                                            initFuncs.push(elm.init);
                                        }

                                        if (elm.gload) {
                                            onGridLoad(elm.gload);
                                        }

                                        fchtml += inlHtml;
                                    }
                                });
                        }

                        if (!elen && prop) {
                            defaultFilter(prop);
                        }

                        gtd.html(rdiv('o-fcell', fchtml));

                        if (elen > 1) {
                            gtd.find('.o-fcell').children().first().addClass('o-first');
                        }

                        function defaultFilter(prop, elm) {
                            var data = guc(o, prop, frow);
                            if (data) {
                                var id = idFunc(prop);

                                var pval = fopt.model[prop] || '';

                                var mainf = awe.radioList;
                                var type = elm && elm.type;

                                var mod = awem.odropdown;
                                var tag = {
                                    clearBtn: ifnul(elm && elm.clearBtn, clearbtns),
                                    reorderable: 1
                                };

                                var multi = 0;
                                if (type === 'multiselect') {
                                    mainf = awe.checkboxList;
                                    multi = 1;
                                    mod = awem.multiselect;
                                }
                                else if (type === 'multichk') {
                                    mainf = awe.checkboxList;
                                    multi = 1;
                                    mod = awem.multichk;
                                    tag.asmi = 0;
                                }

                                var fopt2 = {
                                    id: id,
                                    nm: prop,
                                    dataFunc: function () { return frowData(o, prop); },
                                    md: mod,
                                    tag: tag
                                };

                                fchtml += lsted(id, awef.sval(pval), '', prop, multi);
                                initFuncs.push(function () { mainf(fopt2); });
                            }
                        }
                    });
                cx.bind = null;

                function onGridLoad(f) {
                    function handl(e) {
                        if (trg(e).is(grid)) {
                            f(cx);
                        }
                    }

                    grid.on(saweload, handl);

                    loadHandls.push(handl);
                }

                loop(strInitFuncs, function (sf) {
                    eval(sf);
                });

                loop(initFuncs, function (f) {
                    f(cx);
                });
            });
        }
    }

    function gridAutoMiniPager(oo, forceMini) {
        var grid = oo.v;
        var footer = grid.find('.awe-footer');
        if (!footer.length) return;
        var api = dapi(grid);
        var original = api.buildPager;

        var miniPager = function (o) {
            var pageCount = o.lrs.pc;
            var page = o.pg || 1;
            if (o.lrs.pgn) {
                var result = se;

                result += renderButton(1, icon('o-arw double left'), 0, page < 2, 'ba');
                result += renderButton(page - 1, icon('o-arw left'), 0, page < 2, 'b');

                result += renderButton(page, page, 1, 0, 'c');

                result += renderButton(page + 1, icon('o-arw right'), 0, pageCount <= page, 'f');
                result += renderButton(pageCount, icon('o-arw double right'), 0, pageCount <= page, 'fa');

                var $pager = grid.find('.awe-pager');
                $pager.html(result);

                $pager.find('.awe-btn').on(sclick, function () {
                    var p = $(this).data('p');
                    var act = $(this).data('act');
                    if (!o.ldg) {
                        $.when(dapi(grid).load({ start: function () { o.pg = parseInt(p); } }))
                            .done(function () {
                                var fbtn = $pager.find('[data-act=' + act + "]");
                                if (fbtn.is(':disabled')) {
                                    fbtn = $pager.find('.awe-btn:not(:disabled)').first();
                                }

                                if (!$(':focus').length) {
                                    fbtn.focus();
                                }
                            });
                    }
                });

                setTimeout(function () {
                    api.lay();
                }, 10);
            }
        };

        decideSwitch();

        if (!forceMini) {
            bind(oo, $win, 'resize awedomlay', tryminipager);
            bind(oo, grid, saweload, function (e) {
                if (isTrgIt(e, grid)) tryminipager();
            });
        }

        function tryminipager() {
            checkObjPres(oo, grid, tryminipager);
            if (decideSwitch()) {
                api.buildPager(oo);
            }
        }

        function decideSwitch() {
            var cval = api.buildPager;
            var pc = oo.lrs ? oo.lrs.pc : 0;
            var nval = forceMini || $win.width() < 1000 && pc > 5 ? miniPager : original;
            api.buildPager = nval;
            return nval !== cval;
        }

        function icon(icls) {
            return '<span class="' + icls + '" aria-hidden="true"></span>';
        }

        function renderButton(page, caption, selected, disabled, act) {
            var clss = "awe-btn ";
            if (selected) clss += saweselected + ' ';
            if (disabled) clss += "awe-disabled ";
            var dis = disabled ? sdisabled : se;
            return rbtn(clss, caption, 'data-p="' + page + '" data-act="' + act + '" ' + dis);
        }
    }

    function gridkeynav(o) {
        var grid = o.v;
        var api = dapi(grid);

        grid.addClass('keynav');
        grid.attr('tabindex', '0');
        var con = grid.find(sawecontentc);
        var sctrl = slist(con, { sel: sawerowc, fcls: sfocus, sc: 'n', topf: topFunc, botf: botFunc, enter: onenter });

        function topFunc() {
            chpage(-1);
        }

        function botFunc() {
            chpage(1);
        }

        function onenter(e, focused) {
            
            if (focused.length) {
                prevDef(e);
                var shift = e.shiftKey;

                if (!shift && focused.find('.awe-movebtn').length) {

                    var next = pickAvEl([focused.next(), focused.prev()]);

                    focused.removeClass(sfocus);
                    focused.find('.awe-movebtn').click();

                    if (next) {
                        sctrl.focus(next);
                    }

                } else {
                    focused.click();
                }

                if (shift) {
                    var popup = grid.closest('.awe-popup');
                    if (len(popup)) {
                        var po = dto(popup);
                        if (po.api && po.api.ok) {
                            focused.addClass(saweselected);
                            po.api.ok();
                        }
                    }
                }
            }
        }

        var nofocus;
        grid.keydown(function (e) {
            var tg = trg(e);
            var k = which(e);
            if ((k === keyDown || k === keyUp) && tg.is('.awe-btn:not(.o-ddbtn)')) {
                tg = grid;
                grid.focus();
            }

            if (tg.is(grid)) {
                var keys = [40, 38, 35, 36, 34, 33];

                sctrl.keyh(e);

                if ($.inArray(k, keys) !== -1) {
                    prevDef(e);
                }

                if (k === 34) {
                    // page down
                    chpage(1);
                } else if (k === 33) {
                    // page up
                    chpage(-1);
                } else if (k === 35) {
                    // end
                    sctrl.focus(grid.find(sawerowc).last());
                    sctrl.scrollToFocused();
                } else if (k === 36) {
                    // home
                    sctrl.focus(grid.find(sawerowc).first());
                    sctrl.scrollToFocused();
                } else if (k === 32) {
                    //space
                    onenter(e, grid.find('.' + sfocus));
                }
            }
        })
            .on('mousedown',
                function () {
                    nofocus = 1;
                    setTimeout(function () { nofocus = 0; }, 100);
                })
            .on('focusin',
                function (e) {
                    if (!nofocus && !trg(e).is(':input')) {
                        sctrl.autofocus();
                    }

                    nofocus = 0;
                })
            .on('focusout',
                function () {
                    sctrl.remf();
                })
            .on(saweload, removeTabIndex);

        function removeTabIndex() {
            grid.find('.awe-footer .awe-btn').each(function () {
                var btn = $(this);
                btn.attr('tabindex', -1);
            });
        }

        function chpage(val) {
            var ldrs = con.children(val > 0 ? '.app' : '.pre');
            if (len(ldrs)) {
                ldrs.children().click();
            }

            if (o.inf || o.ldg) return;

            var p = o.pg;
            if (p < o.lrs.pc && val > 0 || p > 1 && val === -1) {
                $.when(api.load({ oparams: { page: p + val } })).done(function () {
                    var tof = null;
                    if (val < 0) tof = grid.find(sawerowc).last();
                    sctrl.autofocus(tof);
                });
            }
        }
    }

    function multilookupGrid(o) {
        var popup;
        var gridsrl, gridsel;
        var api = o.api;
        o.p.dh = o.p.h;
        api.gsval = getSelectedValue;

        function getSelectedValue() {
            if (gridsel && dto(gridsel).lrs) {
                return gridsel.find(sawerowc).map(function () { return $(this).data('k'); }).get();
            } else {
                return awe.val(o.v);
            }
        }

        api.lay = function () {
            if (gridsrl && gridsel) {

                var resth = popup.find('.awe-scon').height() -
                    outerh(gridsrl) -
                    outerh(gridsel) +
                    outerh(popup) -
                    popup.height();

                var avh = o.avh || popup.height();
                var newh = (avh - resth - 1) / 2;

                setGridHeight(gridsrl, newh);
                setGridHeight(gridsel, newh);
            }
        };

        api.rload = function () {
            dapi(gridsrl).load();
            dapi(gridsel).load();
        };

        o.v.on('awepopupinit', function () {

            gridsrl = null;
            gridsel = null;
            popup = o.p.d;

            popup.on(sclick, '.awe-movebtn', function (e) {
                var tg = trg(e);
                var gridfrom = gridsel, gridto = gridsrl;
                if (tg.closest(gridsrl).length) {
                    gridfrom = gridsrl;
                    gridto = gridsel;
                }

                var trgRow = tg.closest(sawerowc);


                gridto.find('.awe-content .awe-tbody')
                    .prepend(dapi(gridto).renderRow(dapi(gridfrom).model(trgRow)));

                trgRow.remove();
                movedGridRow(gridfrom, gridto);
            });

            popup.on('awefinit', function (e) {
                var it = trg(e);
                if (it.is(sawegridcls)) {
                    var gdo = dto(it);
                    gdo.pro = dto(popup);

                    var getSelected = function () { return { selected: getSelectedValue() }; };
                    gdo.parameterFunc = gdo.parameterFunc ? gdo.parameterFunc.concat(getSelected) : [getSelected];

                    if (it.is('.awe-srl')) {
                        gridsrl = it;
                    }
                    else if (it.is('.awe-sel')) {
                        gridsel = it;
                        api.lay();
                    }
                }
            });
        });

        o.p.wlim = 5;
    }

    function lookupKeyNav(o) {
        var api = o.api;
        o.v.on('awepopupinit', function () {
            handleCont(o.srl.closest('.awe-list'));

            if (o.sel) {
                handleCont(o.sel.closest('.awe-list'));
            }

            function handleCont(cont) {
                cont.attr('tabindex', 0);

                var sctrl = slist(cont, { sel: '.awe-li', enter: onenter });

                function onenter(e, focused) {
                    prevDef(e);
                    var shift = e.shiftKey;
                    if (focused.is('.awe-morecont')) {
                        var prev = focused.prev();
                        focused.parent()
                            .one(saweload, function () {
                                sctrl.focus(pickAvEl([prev.next(), prev, cont.find('.awe-li').first()]));
                            });
                        focused.find('.awe-morebtn').click();
                    } else if (focused.find('.awe-movebtn').length && !shift) {
                        var tofocus = pickAvEl([focused.next(), focused.prev()]);

                        focused.removeClass(sfocus);
                        focused.find('.awe-movebtn').click();

                        if (tofocus) {
                            sctrl.focus(tofocus);
                        }
                    }
                    else {
                        focused.click();
                        if (shift) {
                            focused.addClass(saweselected);
                        }
                    }

                    if (shift) {
                        api.ok();
                    }
                }

                function handleKeys(e) {
                    var keys = [40, 38, 35, 36, 34, 33];
                    if (sctrl.keyh(e) || $.inArray(which(e), keys) !== -1) {
                        prevDef(e);
                    }

                    if (which(e) === 32) { // space
                        onenter(e, cont.find('.focus'));
                    }
                }

                cont.keydown(handleKeys);
                cont.on('focusout', function () {
                    cont.find('.focus').removeClass(sfocus);
                }).on(skeyup, function (e) {
                    if (which(e) === keyTab)
                        sctrl.autofocus();
                });
            }
        });
    }

    function lookupGrid(o) {
        var popup;
        var grid;
        var api = o.api;

        api.gsval = function () {
            return popup.find(saweselectedc).data('k');
        };

        api.lay = function () {
            if (grid) {
                var resth = popup.find('.awe-scon').height() - outerh(grid) + outerh(popup) - popup.height();
                var newh = (o.avh || outerh(popup)) - resth;
                setGridHeight(grid, newh);
            }
        };

        api.rload = function () {
            dapi(grid).load();
        };

        o.v.on('awepopupinit', function () {
            popup = o.p.d;
            grid = null;

            popup.on('dblclick', sawerowc, function (e) {
                if (!istrg(e, '.awe-nonselect')) {
                    o.api.sval($(this).data('k'));
                }
            });

            popup.on(saweinit, function (e) {
                var g = trg(e);
                if (g.is(sawegridcls)) {
                    dto(g).pro = dto(popup);
                    grid = g;
                    api.lay();
                }
            });
        });

        o.p.wlim = 5;
    }

    function tbtns(o) {
        var tabs = $('#' + o.id);

        function laybtns() {
            var av = tabs.width();
            var w = av;
            tabs.find('.awe-tabsbar br').remove();
            var btns = tabs.find('.awe-tab-btn');
            var l = btns.length;
            var isFirst = 1;

            for (var i = l - 1; i >= 0; i--) {
                var btn = btns.eq(i);
                w -= outerw(btn);

                if (w < 0) {
                    if (isFirst) continue;
                    btn.after('</br>');
                    isFirst = 1;
                    i++;
                    w = av;
                } else {
                    isFirst = 0;
                }
            }
        }

        tabs.on('awerender', function (e) {
            if (!trg(e).is(tabs)) return;
            laybtns();
        });

        $win.off('resize awedomlay', laybtns).on('resize awedomlay', laybtns);
    }

    function dtp(o) {
        var id = o.id;
        var cmid = id + 'cm';
        var cyid = id + 'cy';        

        var popupId = id + '-awepw';
        var shov = 'o-hov';
        var shovc = '.' + shov;
        var monthNames = cd().Months;
        var useTime;

        var dayNames = cd().Days.slice(0);

        if (awem.fdw) {
            dayNames.push(dayNames.shift());
        }

        var prm = o;
        var input = o.v;
        var openb = o.f.find('.awe-dpbtn');
        var selDates = [];
        var selCount = prm.selectRange ? 2 : 1;
        var inline = prm.inline;
        var inlCont = o.f.find('.awe-ilc');
        var rtl = o.rtl;
        var nxtcls = '.o-mnxt';
        var prvcls = '.o-mprv';

        if (rtl) {
            var c = nxtcls;
            nxtcls = prvcls;
            prvcls = c;
        }

        var cmdd, cydd;
        var timedds = {};
        var isOpening;
        var currDate;
        var today;

        var numberOfMonths;
        var defaultDate;
        var dateFormat;
        var changeYear;
        var changeMonth;
        var minDate;
        var maxDate;
        var amaxDate;
        var yearRange;
        var showTodayBar;
        var isAmPm;
        var dayFunc;

        var popup, cont;
        var wasOpen;
        var kval;

        init();

        input.attr('autocomplete', 'off');

        bind(o, input, skeyup, keyuph);
        bind(o, input, skeydown, inpkeyd);
        var dateRangeSep = '-';

        function parseInput(corr) {
            var sval = input.val().trim();
            var vals = [];
            
            // daterange separator
            var rangeSep = dateFormat.split(dateRangeSep).length;

            for (var i = 0; i < sval.length; i++) {
                if (sval[i] === dateRangeSep && !--rangeSep) {
                    var val = sval.slice(0, i - 1).trim();
                    val && vals.push(val);
                    val = sval.slice(i + 1).trim();
                    val && vals.push(val);
                    break;
                }
            }

            sval && !vals.length && vals.push(sval);

            var noval, needToSetDate, prevd, res = [];
            loop(vals, function (v) {

                var d1 = tryParseToDate(v, currDate, minDate, maxDate);
                if (!d1) {
                    noval = true;
                    return false;
                }

                if (prevd && d1 < prevd) {
                    d1 = prevd;
                }

                prevd = d1;

                // parse wo limits, check diff
                var d2 = tryParseToDate(v);
                if (!d2 || datesDif(d1, d2) || formatDate(dateFormat, d1) !== v) {                    
                    needToSetDate = true;
                }

                res.push(d1);
            });

            if (noval && corr) {
                input.val(se).change();
                return;
            }

            if (needToSetDate && corr) {                
                o.api.setDate(res);
            } else {                
                setVal(res);
            }
        }

        bind(o, input, schange, function () {            
            parseInput(1);
        });

        openb.on(skeydown, function (e) {
            var key = which(e);
            if (key === keyEnter) {
                wasOpen = !isOpen();
            }

            if (keynav(key)) {
                prevDef(e);
            }
        }).on(skeyup, keyuph);

        if (inline) {
            openDtp();
        } else {
            if (!isMobile()) {
                bind(o, input, sclick, openDtp);
            }

            openb.on(sclick, openDtp);
        }

        o.api.getDate = function () {
            if (!len(selDates)) return;
            if (len(selDates) === 1) return selDates[0];
            return selDates;
        };

        o.api.setDate = function (val) {
            if (!Array.isArray(val)) val = [val];

            input.val(formatDates(val)).change();
        };

        function formatDates(val) {
            return qsel(val, function (v) { return formatDate(dateFormat, v) }).join(' ' + dateRangeSep + ' ');
        }

        o.api.render = updateCalendar;

        function updateCurrDateToSel() {
            if (len(selDates)) {
                setCurrDate(selDates[len(selDates) - 1]);
            }
        }

        function setVal(newVal) {
            if (len(newVal)) {
                var i = 0;
                if (len(newVal) > 1 && (len(selDates) < 2 || !dateEq(newVal[1], selDates[1]))) {
                    i = 1;
                } else if (len(selDates) && dateEq(newVal[0], selDates[0])) {
                    i = -1;
                }

                
                if (i >= 0)
                {

                    // match calendar to input
                    setCurrDate(newVal[i]);
                }   
            }

            if (!areScritEq(selDates, newVal)) {
                selDates = newVal;
                updateCalendar();
            }
        }

        function setCurrDate(newDate) {
            if (minDate && newDate < minDate) {
                currDate = cdate(minDate);
            }
            else if (amaxDate && newDate > amaxDate) {
                currDate = cdate(amaxDate);
            } else {
                currDate = cdate(newDate);
            }
        }

        function moveHov(dir) {
            var pivot = getHov();
            var sel = '.o-enb';
            if (cont.find(nxtcls).is(':enabled')) {
                sel = '.o-mnth:first ' + sel;
            }

            var list = cont.find(sel);

            var indx = list.index(pivot) + dir;
            var n = list.eq(indx);

            if (!n.length || indx < 0) {
                n = 0;
                var cls = dir > 0 ? nxtcls : prvcls;
                var fl = dir > 0 ? 'first' : 'last';
                var nbtn = cont.find(cls);

                if (nbtn.is(':enabled')) {
                    cont.find(cls).click();
                    n = cont.find('.o-mnth:first .o-enb:' + fl);
                }
            }

            if (n && n.length) {
                cont.find(shovc).removeClass(shov);
                n.addClass(shov);
            }
        }

        function keynav(key) {
            var res = 0;
            if (isOpen()) {
                if (key === keyDown) {
                    moveHov(1);
                    res = 1;
                }
                else if (key === keyUp) {
                    moveHov(-1);
                    res = 1;
                }
            }

            if (res) cont.addClass('o-nhov');
            return res;
        }

        function inpkeyd(e) {
            var key = which(e);

            if (keynav(key)) {
                prevDef(e);
            }

            if (!isOpen()) {
                if (key === keyDown || key === keyUp) {
                    openDtp(e);
                }
            } else {
                if (key === keyEnter) {
                    prevDef(e); // stop form post
                }
            }

            if (!isAmPm) {
                // / / . . - -
                awe.pnn(e, [191, 111, 190, 110, 189, 109]);
            }

            kval = input.val();
        }

        function keyuph(e) {
            var k = which(e);

            if (isOpen()) {
                if (k === keyEnter) {
                    if (!wasOpen) {
                        getHov().click();
                    }
                } else if (!inline && k === keyEsc) {
                    dapi(popup).close();
                    e.stopPropagation();
                }
                else if (input.val() !== kval) {                    
                    parseInput();
                }
            }

            wasOpen = 0;
        }

        function isOpen() {
            return cont && cont.closest('body').length;
        }

        function getHov() {
            var h = cont.find(shovc);
            if (!h.length) h = cont.find('.o-enb:hover');
            if (!h.length) h = cont.find('.o-enb.o-selday');
            if (!h.length) h = cont.find('.o-enb.o-tday');
            if (!h.length) h = cont.find('.o-enb:first');

            return h;
        }

        // return 0 on failure
        function tryParseToDate(sval, baseDate, mind, maxd) {
            var val = 0;
            try {
                if (!sval) return 0;
                val = parseDate(dateFormat, sval, baseDate, mind, maxd);
                if (isNaN(val)) val = 0;
            }
            catch (err) {
                /*val will be 0 on failure*/
            }

            return val;
        }        

        function init() {
            today = cdate();
            remTime(today);

            numberOfMonths = prm.numberOfMonths || 1;
            defaultDate = prm.defaultDate;
            dateFormat = prm.format || awem.dateFormat;

            dateFormat = dateFormat.split('D').join('d').split('Y').join('y');

            isAmPm = getIsAmPm(dateFormat);

            changeYear = prm.changeYear;
            changeMonth = prm.changeMonth;
            minDate = prm.minDate;
            maxDate = prm.maxDate;
            yearRange = prm.yearRange;            
            showTodayBar = prm.showTodayBar;
            dayFunc = prm.dayf;

            if (isNull(showTodayBar)) showTodayBar = 1;
            
            useTime = prm.useTime;

            if (prm.min) {
                minDate = tryParseToDate(prm.min);
            }

            if (prm.max) {
                maxDate = tryParseToDate(prm.max);
            }

            if (minDate) {
                remTime(minDate);
            }

            if (maxDate) {
                remTime(maxDate);
                amaxDate = cdate(maxDate);
                amaxDate.setMonth(amaxDate.getMonth() - numberOfMonths + 1);
                if (minDate && amaxDate < minDate) {
                    amaxDate = minDate;
                }
            }

            initCurrDate();
        }

        function initCurrDate() { 
            updateCurrDateToSel();
            parseInput();
            setCurrDate(currDate || defaultDate || today);

            remTime(currDate);            
        }

        function openDtp(e) {

            if (isOpen() || isOpening) return;
            isOpening = 1;

            init();

            if ($('#' + popupId).length) {
                dapi($('#' + popupId)).destroy();
            }

            cont = $(rdiv('o-dtp')).hide();
            cont.appendTo($('body'));

            if (inline) {
                cont.addClass('o-inl');
            }

            if (inline) {
                inlCont.html(cont);
            } else {
                popup = $('<div id="' + popupId + '"/>');
                popup.append(cont);

                var ctf = input;
                if (input.is('[readonly]')) {
                    ctf = openb;
                }

                if (e && istrg(e, openb)) {
                    ctf = openb;
                }

                awem.dropdownPopup({
                    opener: o.f,
                    fcs: ctf,
                    rtl: rtl,
                    d: popup, id: popupId, minw: 'auto', popupClass: 'o-dtpp', nf: 1,
                    tag: { Dd: 1, MinWidth: '150px' }
                });

                dapi(popup).open({ e: e });
            }

            cont.html(render(currDate));

            changeMonth && awe.radioList({ id: cmid, nm: cmid, dataFunc: monthItems, md: awem.odropdown });

            changeYear && awe.radioList({ id: cyid, nm: cyid, dataFunc: yearItems, md: awem.odropdown, tag: { Asmi: -1 } });

            if (useTime) {
                runTmpJs(id, isAmPm);
            }

            cmdd = $('#' + cmid);
            cydd = $('#' + cyid);

            setTimeDds(timedds, id);            

            updateCalendar(1);

            cont.on(smousemove, function () {
                cont.removeClass('o-nhov');
                cont.find(shovc).removeClass(shov);
            });

            var clsel = '.o-mday:not(.o-dsb)';
            var lstd = 0;
            (selCount > 1) &&
                cont.on(smousemove, function (ev) {
                    var t = trg(ev);
                    if (len(selDates) === 1) {

                        var td = t.closest(clsel);
                        if (!len(td)) {
                            lstd && updateCalendar();
                            lstd = 0;
                            return;
                        }

                        var osd = selDates.slice(0);
                        var d = new Date(td.data('t'));

                        if (lstd !== d.getTime()) {
                            lstd = d.getTime();

                            selDates.push(d);
                            selDates.sort(function (d1, d2) {
                                return d1 - d2;
                            });

                            updateCalendar();

                            selDates = osd;
                        }
                    }
                });

            function setExistingTime(newVal) {
                if (len(selDates)) {
                    var prevDate = selDates[0];
                    newVal.setHours(prevDate.getHours());
                    newVal.setMinutes(prevDate.getMinutes());
                }
            }

            function setDateAndClose(newVal) {
                newVal && setExistingTime(newVal);

                if (selCount === len(selDates)) {
                    selDates = [];
                }

                if (newVal) {
                    selDates.push(newVal);
                    selDates.sort(function (d1, d2) {
                        return d1 - d2;
                    });
                }

                updateCurrDateToSel();

                // prevent outclick close because can't find closest popup
                setTimeout(updateCalendar, 1);

                input.val(formatDates(selDates));

                if (len(selDates) === selCount || len(selDates) === 0) {
                    awe.ach(o);
                    popup && dapi(popup).close();
                }
            }

            // day click
            cont.on(sclick,
                clsel,
                function () {
                    var td = $(this);
                    
                    var newVal = new Date(td.data('t'));                    
                    applyPopupTime(newVal);
                    
                    setDateAndClose(newVal);
                });

            cont.on(sclick,
                nxtcls,
                function () {
                    var ndate = cdate(currDate);
                    incMonth(ndate, 1);
                    setCurrDate(ndate);
                    updateCalendar();
                });            

            cont.on(sclick,
                prvcls,
                function () {
                    var ndate = cdate(currDate);
                    incMonth(ndate, -1);
                    setCurrDate(ndate);
                    updateCalendar();
                });

            cont.on(sclick, '.o-tdbtn', function () {
                var opt = { d: new Date() };
                dayFunc && dayFunc(opt);
                if (!opt.dsb) {
                    setDateAndClose(applyPopupTime(new Date()));
                }                
            })
            .on(sclick, '.o-clrbtn', function () {                
                setDateAndClose();                
            });

            cont.show();

            popup && dapi(popup).lay();

            cmdd.change(function () {
                var ndate = cdate(currDate);
                ndate.setDate(1);
                var val = cmdd.val();
                if (val) {
                    ndate.setMonth(val);
                    setCurrDate(ndate);
                    updateCalendar();
                }
            });

            cydd.change(function () {
                var ndate = cdate(currDate);
                ndate.setDate(1);
                var val = cydd.val();
                if (val) {
                    ndate.setFullYear(val);
                    setCurrDate(ndate);
                    updateCalendar();
                }
            });

            setOnTimeChange(timedds, timeChange);

            isOpening = 0;
        }
        
        function applyPopupTime(ndate) {
            if (!useTime) return ndate;
            return applyDispTime(ndate, timedds, isAmPm);
        }        

        function timeChange() {
            var ndate = len(selDates) ? selDates[0] : remTime(new Date());

            applyPopupTime(ndate);

            //setCurrDate(ndate);
            input.val(formatDates(selDates));
        }

        function updateCalendar(oinit) {
            if (!isOpen()) return;
            var monthcs = cont.find('.o-mnth');
            var mlen = monthcs.length;
            monthcs.each(function (i, el) {
                var day = cdate(currDate);
                incMonth(day, i);
                var mc = $(el);
                mc.find('.o-tb').html(renderDaysTable(day, mlen));

                if (i || !changeYear) mc.find('.o-yhd').html(pad(year(day)));
                if (i || !changeMonth) mc.find('.o-mhd').html(pad(monthName(day)));

                if (mlen === i + 1) {
                    var ldm = lastDayOfMonth(day);
                    setDisabled(cont.find(nxtcls), maxDate && ldm >= maxDate);
                }
            });

            updateCalendarControls();

            if (!oinit) {
                popup && dapi(popup).lay();
            }
        }

        function updateCalendarControls() {
            var fdm = firstDayOfMonth(currDate);
            setDisabled(cont.find(prvcls), minDate && fdm <= minDate);

            changeMonth && dapi(cmdd.val(currDate.getMonth())).load();
            changeYear && dapi(cydd.val(year(currDate))).load();

            if (useTime) {
                var timeVal = getTimeDispVal();

                dapi(timedds.hour.val(timeVal[0])).load();
                dapi(timedds.min.val(timeVal[1])).load();

                if (isAmPm) {
                    dapi(timedds.tt.val(timeVal[2])).load();
                }
            }
        }

        function yearItems() {
            var y = year(currDate);

            var res = [];
            var startYear = y - 30;
            var endYear = y + 30;

            if (yearRange) {
                var yra = yearRange.split(":");
                startYear = calcYear(yra[0], y, year(today));
                endYear = calcYear(yra[1], y, year(today));
            }

            if (minDate) {
                startYear = Math.max(startYear, year(minDate));
            }

            if (amaxDate) {
                endYear = Math.min(endYear, year(amaxDate));
            }

            for (var i = startYear; i <= endYear; i++) {
                res.push({ c: i, k: i });
            }

            return res;
        }

        function monthItems() {
            var allowed = null;
            if (minDate || amaxDate) {
                var min = null, max = null;

                if (amaxDate) {
                    max = cdate(amaxDate);
                    max.setDate(1);
                }

                if (minDate) {
                    min = cdate(minDate);
                    min.setDate(1);
                }

                var start = cdate(currDate);

                start.setDate(1);
                start.setMonth(0);
                allowed = {};

                for (var j = 0; j < 12; j++) {
                    if ((!min || start >= min) && (!max || start <= max)) {
                        allowed[j] = 1;
                    }

                    incMonth(start, 1);
                }
            }

            var res = [];
            for (var i = 0; i < monthNames.length; i++) {
                if (!allowed || allowed[i])
                    res.push({ c: monthNames[i], k: i });
            }

            return res;
        }

        function render(d) {
            var res = se;
            for (var i = 0; i < numberOfMonths; i++) {
                var day = cdate(d);
                incMonth(day, i);

                res += rdiv('o-mnth', renderMonth(day, i === 0, i === numberOfMonths - 1), 'data-i="' + i + '"');
            }

            return res;
        }

        function renderDaysTable(pivotDay, mlen) {
            var fdm = firstDayOfMonth(pivotDay);
            var ldm = lastDayOfMonth(pivotDay);

            var pmd0 = startOfWeek(fdm);
            var nm1 = endOfWeek(ldm);

            var day = pmd0;

            var table = se;

            function renderDay(d) {
                var date = d.getDate();
                var cls = 'o-day';
                var enb = 0;
                var out = 0;
                if (d < fdm || d > ldm) {
                    cls += ' o-outm';
                    out = 1;
                } else {
                    cls += ' o-mday';
                    enb = 1;
                }

                if (d <= today && d >= today && !out && mlen) {
                    cls += ' o-tday';
                }

                var opt = { cls: cls, d: d };

                if (dayFunc) {
                    dayFunc(opt);
                    cls = opt.cls;
                }

                if (minDate && d < minDate || maxDate && d > maxDate || opt.dsb) {
                    cls += ' o-dsb';
                } else if (enb) {

                    cls += ' o-enb';

                    var sdl = len(selDates);

                    loop(selDates, function (sd) {
                        if (dateOnlyEq(d, sd)) {
                            //dateOnlyEq(d, sd)
                            cls += ' o-selday';
                        }
                    });

                    if (sdl > 1 && d > selDates[0] && d < selDates[1]) {
                        cls += ' o-rgday';
                    }
                }

                return '<td class="' +
                    cls +
                    '" data-t="' +
                    d.getTime() +
                    '"><div>' +
                    date +
                    '</div></td>';
            }

            table += '<tr class="o-wdays">';
            for (var di = 0; di < dayNames.length; di++) {
                table += '<td>' + dayNames[di] + '</td>';
            }
            table += '</tr>';

            var i = 1;
            var rowstarted = 0;
            var rowCount = 0;
            while (day <= nm1 || rowCount < 6) {
                if (!rowstarted) {
                    table += '<tr>';
                    rowstarted = 1;
                }

                table += renderDay(day);

                if (i === 7) {
                    table += '</tr>';
                    rowstarted = 0;
                    i = 0;
                    rowCount++;
                }

                nextDay(day);
                i++;
            }

            return table;
        }

        function renderMonth(pivotDay, first, last) {
            var mbtn = function (cls, icls) {
                return rbtn('awe-btn o-cmbtn ' + cls, '<i class="o-arw ' + icls + '"></i>');
            };

            var topbar = '<div class="o-mtop">';

            if (first) {
                topbar += mbtn(rtl ? 'o-mnxt' : 'o-mprv', 'left');
            }

            var mval = pivotDay.getMonth();
            var yval = year(pivotDay);

            topbar += '<div class="o-ym"><div class="o-mhd">' +
                (first && changeMonth ? lsted(cmid, mval, 'o-cm') : pad(monthName(pivotDay))) +
                '</div>' +
                '<div class="o-yhd">' +
                (first && changeYear ? lsted(cyid, yval, 'o-cy') : pad(yval)) +
                '</div></div>';

            if (last) {
                topbar += mbtn(rtl ? 'o-mprv' : 'o-mnxt', 'right');
            }

            topbar += '</div>';

            var res = topbar + '<table class="o-tb"></table>';

            if (useTime && first) {
                var timeVal = getTimeValArr(getFirstSelDate(), isAmPm);
                
                res += rdiv('o-cldtmp', genTimeEd(timeVal, id, isAmPm));
            }            

            if (first && showTodayBar) {
                
                res += '<div class="o-cldbar"><button type="button" class="awe-btn o-clrbtn">' + cd().Clear + '</button>' +
                    '<button type="button" class="awe-btn o-tdbtn">' + cd().Today + '</button>' +
                    '</div>';
            }

            return res;
        }

        function getTimeDispVal() {
            return getTimeValArr(getFirstSelDate(), isAmPm);
        }

        function getFirstSelDate() {
            return len(selDates) ? selDates[0] : new Date();
        }

        function monthName(pivotDay) {
            var mval = pivotDay.getMonth();
            return monthNames[mval];
        }

        function pad(s) {
            return "<span class='o-txt'>" + s + "</span>";
        }

        function calcYear(fstr, cy, ty) {
            function f(res, i, fstr, cy, ty) {
                if (fstr[i] === 'c')
                    return f(cy, i + 1, fstr, cy, ty);
                if (fstr[i] === '-' || fstr[i] === '+')
                    if (res)
                        res = res + parseInt(fstr.substring(i));
                    else
                        res = ty + parseInt(fstr.substring(i));
                else return parseInt(fstr);

                return res;
            }

            return f(0, 0, fstr, cy, ty);
        }

        // utils methods
        function startOfWeek(date) {
            var dat = cdate(date);

            var day = dat.getDay();
            var diff = dat.getDate() - day;

            if (awem.fdw) {
                diff += day === 0 ? -6 : 1;
            }

            dat.setDate(diff);
            return dat;
        }

        function endOfWeek(d) {
            var dat = cdate(startOfWeek(d));
            dat.setDate(dat.getDate() + 6);
            return dat;
        }

        function firstDayOfMonth(d) {
            var dat = cdate(d);
            dat.setDate(1);
            return dat;
        }

        function remTime(d) {
            d.setHours(0, 0, 0, 0);
            return d;
        }

        function nextDay(d) {
            d.setDate(d.getDate() + 1);
        }

        function incMonth(d, m) {
            d.setDate(1);
            d.setMonth(d.getMonth() + m);
            return d;
        }
    }

    function lsted(id, val, cls, name, multi) {
        var clss = 'ajaxradiolist';
        var inpclss = 'awe-val';
        var attr = '';
        if (multi) {
            clss = 'ajaxcheckboxlist';
            inpclss += ' awe-array';
            attr = disbAttr({});
        }

        name = name || '';
        return rdiv('awe-' + clss + '-field awe-field ' + (cls || se), '<input id="' + id +
            '" class="' + inpclss + '" type="hidden" name="' + name + '" value="' + encode(val) + '" ' + attr + ' />' + rdiv('awe-display'));
    }

    function rendOchk(o, radio, checked, type, name, value) {
        var attr = disbAttr(o);
        if (checked) attr += ' checked="checked"';
        if (name) attr += ' value="' + encode(value) + '" name="' + name + '"';
        return '<div tabindex="0" class="o-chk ' + (checked ? schked : se) + '" >' +
            (radio ? se : '<div class="o-chkc"><div class="o-chkico"></div></div>') +
            '</div><input type="' + type + '" ' + attr + ' style="display:none"/>';
    }

    function otggl(o) {
        var yes = readTag(o, 'Yes');
        var no = readTag(o, 'No');
        o.f.find('.o-tgon .o-ccon').html(yes || awe.clientDict.Yes);
        o.f.find('.o-tgoff .o-ccon').html(no || awe.clientDict.No);
        return ochk(o, '.o-tg');
    }

    function ochk(o, cls) {
        var v = o.v;
        var chk = o.f.find(cls || '.o-chk');

        function toggle() {
            if (o.enb) {
                o.f.find(':checkbox').click();
            }
        }

        chk.on(sclick, function () {
            //chk.blur();
            toggle();
        });

        chk.on(skeydown + ' ' + skeyup, function (e) {
            if (which(e) === keyEnter || which(e) === keySpace) {
                prevDef(e);
                if (e.type === skeyup)
                    toggle();
            }
        });

        var first = 1;
        var baseload = o.load;
        o.load = function () {
            baseload();
            var sel = chk.add(chk.closest('.o-chkm'));

            if (v.val() === 'true') {
                var tgg = sel.find('.o-tgg');
                first && tgg.css('transition', 'initial');
                sel.addClass(schked);
                first && setTimeout(function () { tgg.css('transition', ''); }, 300);
            }
            else sel.removeClass(schked);
            first = 0;
        };

        v.on(schange, o.load);

        chk.closest('label').on(sclick, function (e) {
            if (trg(e).closest(chk).length) {
                prevDef(e);
            }
        });
    }

    function ochkl(o) {
        var radio = o.type === 'radioList' ? 1 : 0;
        var type = radio ? 'radio' : 'checkbox';
        var api = o.api;
        api.render = render;
        var disp = o.d;

        var itemFunc = readTag(o, 'ItemFunc');

        function renderItemDisplay(item) {
            return itemFunc ? eval(itemFunc)(item) : econ(item, o);
        };

        radio && disp.addClass('o-rdl') || disp.addClass('o-ochk');

        disp.on(schange, 'input:' + type, function () {
            var el = $(this);
            var ischk = el.is(':checked');
            var chk = el.prev().add(el.closest('.o-chkm'));

            radio && disp.find('.' + schked).removeClass(schked);

            if (ischk) {
                chk.addClass(schked);
            } else {
                !radio && chk.removeClass(schked);
            }

        }).on(skeydown + ' ' + skeyup, '.o-chk', function (e) {
            if (which(e) === keyEnter || which(e) === keySpace) {
                prevDef(e);
                if (e.type === skeyup) {
                    $(this).next().click();
                }
            }
        }).on(sclick, '.o-chk', function () {
            $(this).blur();
        });

        function render() {
            o.d.empty();
            var items = '<ul>';
            var vals = awe.val(o.v);
            loop(o.lrs, function (item) {
                var checked = containsVal(kp(item), vals);
                var schkc = se;
                if (checked) {
                    schkc = schked;
                }

                items += '<li class="o-chkm ' + schkc + '"><label class="awe-label">' +
                    rendOchk(o, radio, checked, type, o.nm, kp(item)) +
                    '<div class="o-con">' + renderItemDisplay(item) + '</div></label></li>';
            });

            items += '</ul>';
            o.d.append(items);
        }
    }

    function gridCstRen(opt) {
        return function (o) {
            eval(opt.mdf)(o);
        };
    }

    // filter row data
    function frowData(o, prop) {
        var res;
        if (o && o.lrs) {
            var tg = o.lrs.tg;
            if (tg && tg.frow) {
                res = guc(o, prop, tg.frow); // frow[prop]                
            }
        }

        return res || [];
    }

    function getDictStr(str) {
        var dict = cd();
        var res = dict[str];

        if (res) return res;
        return str;
    }

    function opData() {
        var kvs = {
            '': getDictStr('Equal'),
            '>': getDictStr('GreaterThan'),
            '>=': getDictStr('GreaterThanOrEqual'),
            '<': getDictStr('LessThan'),
            '<=': getDictStr('LessThanOrEqual'),
        }

        var res = [];

        for (var kv in kvs) {
            res.push({ k: kv, c: kvs[kv] });
        }

        return res;
    }

    return {
        opData: opData,
        frowData: frowData,
        initWave: initWave,
        otggl: otggl,
        empf: emptyFunc,
        ochkl: ochkl,
        ochk: ochk,
        autocomplete: autocomplete,
        dropmenu: dropmenu,
        //formatDate: formatDate,
        dateToStr: function (date, format) {            
            return formatDate(format || awem.dateFormat, date)
        },
        //parseDate: parseDate,
        parseToDate: parseDate,
        toDate: function (str) {
            return parseDate(awem.dateFormat, str);
        },
        setgh: setGridHeight,
        dtp: dtp,
        fdw: 0,
        tbtns: tbtns,
        lookupKeyNav: lookupKeyNav,
        multilookupGrid: multilookupGrid,
        lookupGrid: lookupGrid,
        gridCstRen: gridCstRen,
        gridMovRows: gridMovRows,
        gridFreezeColumns: gridFreezeColumns2,
        dragAndDrop: dragAndDrop,
        dragReor: dragReor,
        clientDict: clientDict,
        gridInlineEdit: gridInlineEdit,
        gridLoading: gridLoading,
        gldng: gldng,
        gridInfScroll: gridInfScroll,
        gridPageSize: gridPageSize,
        gridPageInfo: gridPageInfo,
        gridColSel: gridColSel,
        gridColAutohide: gridColAutohide,
        btnGroup: buttonGroupRadio,
        btnGroupChk: buttonGroupCheckbox,
        multiselect: multiselect,
        colorDropdown: colorDropdown,
        combobox: combobox,
        timepicker: timepicker,
        tmp: tmp,
        menuDropdown: menuDropdown,
        odropdown: odropdown,
        dropdownPopup: dropdownPopup,
        inlinePopup: inlinePopup,
        isMobileOrTablet: isMobileOrTablet,
        multiselb: multiselb,
        multichk: multichk,
        gridAutoMiniPager: gridAutoMiniPager,
        gridMiniPager: gridMiniPager,
        gridKeyNav: gridkeynav,
        notif: notif,
        getColValCont: getColValCont,
        gridFilterRow: gridFilterRow,
        loadgflt: loadgflt,
        slist: slist,
        hovPopup: function (opt) {
            var name = opt.name;

            opt.open = opt.open ||
                function (opener) {
                    var popup = awe.open(name, {
                        opener: opener,
                        dropdown: 1,
                        marg: 5, // used in lay
                        setCont: opt.getCont && function (sd, po) {
                            po.scon.html(opt.getCont(po.opener));
                        },
                        popupClass: 'o-phov'
                    });

                    return popup;
                };

            opt.close = opt.close || function () {
                var api = dapi($('#' + name));
                api && api.close();
            }

            hovOpen(opt);
        },
        hovMenu: function (opt) {
            var dm;
            var op = opt.open;
            opt.open = function (opener) {
                dm = op(opener);
                return dm.cont;
            };
            opt.close = function () {
                dm && dm.destroy();
            };
            opt.shclose = function (e) {
                return !awem.isChildPopup(trg(e), dm.id);
            };
            opt.tclose = 500;
            hovOpen(opt);
        },
        isChildPopup: isChildPopup,
        dateFormat: se,
        version: '7.6.0'
    };
}(jQuery);

//export {awem};