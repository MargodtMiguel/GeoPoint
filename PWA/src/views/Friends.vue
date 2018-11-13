<template>
    <div class="c-friends">
        <div class="c-friends__header">
            <div class="c-friends__header__item">
                <h1>Friends</h1>
            </div>
            <div class="c-friends__header__item">
                <img src="../assets/addFriend.png" alt="">
            </div>
        </div>
            
            
    </div>
</template>

<style lang="scss">
    .c-friends__header{
        display:flex;
        justify-content: space-between;
    }

    .c-friends__header__item{
        margin:40px;
    }

    .c-friends__header__item h1{
        padding:0;
        font-size:3.2em;
    }

    .c-friends__header__item img{
        margin-top:6px;
        width:40px;
    }
</style>

<script>
export default {
    name:'friends'
}
</script>

